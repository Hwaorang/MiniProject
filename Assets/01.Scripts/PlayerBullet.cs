using System.Collections;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] float moveSpeed=8f;
    float lifeTime = 3f;

    Rigidbody2D rb;

    public int damage;
    private float camLimit;
    private Coroutine limitTimeCoroutine;


    private void Awake()
    {        
        rb = GetComponent<Rigidbody2D>();        
        damage = 1;        
    }
    private void OnEnable()
    {
        camLimit = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;


        if(rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.linearVelocity = Vector2.right * moveSpeed;
        }

        if(limitTimeCoroutine != null)
        {
            StopCoroutine(limitTimeCoroutine);
        }

        limitTimeCoroutine = StartCoroutine(LimitTime());
    }
    private void Update()
    {
        if (transform.position.x > camLimit + 0.5f)
        {
            ReturnPool();
        }

    }

    IEnumerator LimitTime()
    {
        yield return new WaitForSeconds(lifeTime);
        ReturnPool();
    }

    

    void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject("Bullet", this.gameObject);
    }
    

    private void OnDisable()
    {
        if(limitTimeCoroutine != null)
        {
            StopCoroutine(limitTimeCoroutine);
            limitTimeCoroutine = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            ReturnPool();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            collision.gameObject.GetComponent<BossController>().TakeDamage(damage);
            ReturnPool();
        }
    }
    
}
