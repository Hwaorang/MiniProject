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

    private bool isReturned = false;

    private void Awake()
    {        
        rb = GetComponent<Rigidbody2D>();        
        damage = 1;
        camLimit = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
    }
    private void OnEnable()
    {

        isReturned = false;

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
        if(isReturned)
        {
            return;
        }

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
        if(isReturned)
        {
            return;
        }    
        isReturned = true;

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
        if(isReturned)
        {
            return;
        }

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
