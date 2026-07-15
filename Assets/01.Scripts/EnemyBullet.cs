using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8f;
    float lifeTime = 3f;

    Rigidbody2D rb;
    int damage;

    private Coroutine limitTimeCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        damage = 1;
    }

    private void OnEnable()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.linearVelocity = Vector2.right * moveSpeed;
        }

        if (limitTimeCoroutine != null)
        {
            StopCoroutine(limitTimeCoroutine);
        }

        limitTimeCoroutine = StartCoroutine(LimitTime());
    } 
    

    IEnumerator LimitTime()
    {
        yield return new WaitForSeconds(lifeTime);
        ReturnPool();
    }

    public void SetDir(Vector2 dir)
    {
        rb.linearVelocity = dir;
    }

    void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject("EnemyBullet", this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            ReturnPool();
        }
    }
}
