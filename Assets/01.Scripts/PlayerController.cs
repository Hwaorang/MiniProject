using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// 플레이어 캐릭터 조작
// 이동 동작 구현

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D collider;
    SpriteRenderer sr;

    [SerializeField] float moveSpeed =5f;
    [SerializeField] int maxHp = 1;
    [SerializeField] int bombCount = 3;
    int nowHp;

    Vector2 dir;

    Camera camera;

    Bomb bomb;


    float immortalTime;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        bomb = new Bomb();
        camera = Camera.main;
        nowHp = maxHp;
        immortalTime = 2.5f;
    }

    
    void Update()
    {
        dir = Vector2.zero;

        if(Keyboard.current.wKey.isPressed)
        {
            dir += Vector2.up;
        }
        if(Keyboard.current.sKey.isPressed)
        {
            dir += Vector2.down;
        }
        if(Keyboard.current.aKey.isPressed)
        {
            dir += Vector2.left;
        }
        if(Keyboard.current.dKey.isPressed)
        {
            dir += Vector2.right;
        }

        if(Keyboard.current.ctrlKey.wasPressedThisFrame)
        {
            TryUseBomb();
        }

        dir = dir.normalized;
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector2 velocity = dir * moveSpeed;
        rb.linearVelocity = LimitMove(velocity);
    }

    Vector2 LimitMove(Vector2 vec)
    {
        Vector3 min = camera.ViewportToWorldPoint(Vector3.zero);
        Vector3 max = camera.ViewportToWorldPoint(Vector3.one);

        Vector2 extent = collider.bounds.extents;

        float minX = min.x + extent.x;
        float minY = min.y + extent.y;

        float maxX = max.x - extent.x;
        float maxY = max.y - extent.y;

        if(transform.position.x <= minX && vec.x <0f)
        {
            vec.x = 0;
        }
        if(transform.position.y <= minY && vec.y <0f)
        {
            vec.y = 0;
        }
        if(transform.position.x >= maxX && vec.x >0f)
        {
            vec.x = 0;
        }
        if(transform.position.y >= maxY && vec.y >0f)
        {
            vec.y = 0;
        }

        return vec;
    }

    public void TakeDamage(int dmg)
    {
        nowHp -= dmg;
        if(nowHp <= 0)
        {
            nowHp = 0;
            Die();
        }

    }

    void Die()
    {
        // 파괴
        // 남은 life 있는지 확인

        GameManager.instance.ReducePlayerLife();
    }

    public void ResetPlayer()
    {
        maxHp = 1;
        bombCount = 3;
        gameObject.GetComponent<PlayerAttack>().level = 0;
        StartCoroutine(Immortal());
        StartCoroutine(Blink());
    }

    // 잠시 무적 처리
    IEnumerator Immortal()
    {
        collider.enabled = false;

        yield return new WaitForSeconds(immortalTime);

        collider.enabled = true;
    }

    IEnumerator Blink()
    {

        for(int i =0; i<3; i++)
        {
            WaitForSeconds wait = new WaitForSeconds(0.5f);

            yield return wait;
            sr.enabled = false;
            yield return wait;
            sr.enabled = true;
        }
        
    }

    void TryUseBomb()
    {
        if(bombCount>0)
        {
            bombCount--;
            bomb.UseBomb();
            UIManager.instance.SetBombText(bombCount.ToString());
        }
        else
        {
            return;
        }
    }
}
