using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] float moveSpeed=8f;
    float lifeTime;

    Rigidbody2D rb;

    public int damage;

    void Start()
    {
        lifeTime = 3f;
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector3.right * moveSpeed;
        damage = 1;
        Destroy(gameObject,lifeTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
