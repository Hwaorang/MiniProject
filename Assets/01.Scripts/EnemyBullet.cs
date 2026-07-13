using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    int damage;

    Rigidbody2D rb;

    float lifeTime;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
        lifeTime = 3f;
        Destroy(gameObject, lifeTime);
    }

    public void SetDir(Vector2 dir)
    {
        rb.linearVelocity = dir;
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
