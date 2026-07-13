using UnityEngine;
using UnityEngine.InputSystem;

// 플레이어 캐릭터 조작
// 이동 동작 구현

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D collider;

    [SerializeField] float moveSpeed =5f;
    [SerializeField] int maxHp = 1;

    int nowHp;

    Vector2 dir;

    Camera camera;

    
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        camera = Camera.main;
        nowHp = maxHp;
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
        // 연출
        // 파괴
        // 남은 life 있는지 확인
    }
}
