using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8f;
    float lifeTime = 3f;

    Rigidbody2D rb;
    int damage;

    Camera cam;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    private float padding = 0.3f;

    private Coroutine limitTimeCoroutine;

    private bool isReturned = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        damage = 1;
        cam = Camera.main;
    }

    private void OnEnable()
    {
        isReturned = false;

        if(cam != null)
        {
            Vector3 bottomLeft = cam.ViewportToWorldPoint(Vector3.zero);
            Vector3 topRight = cam.ViewportToWorldPoint(Vector3.one);

            xMin = bottomLeft.x;
            xMax = topRight.x;
            yMin = bottomLeft.y;
            yMax = topRight.y;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.linearVelocity = Vector2.left * moveSpeed;
        }

        if (limitTimeCoroutine != null)
        {
            StopCoroutine(limitTimeCoroutine);
        }

        limitTimeCoroutine = StartCoroutine(LimitTime());
    }

    private void Update()
    {
        if (isReturned)
        {
            return;
        }

        if (transform.position.x < xMin - padding ||
            transform.position.x > xMax + padding ||
            transform.position.y < yMin - padding ||
            transform.position.y > yMax + padding) 
        {
            ReturnPool();
        }
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
        if(isReturned)
        {
            return;
        }
        isReturned = true;

        ObjectPoolManager.instance.ReturnObject("EnemyBullet", this.gameObject);
    }

    private void OnDisable()
    {
        if (limitTimeCoroutine != null)
        {
            StopCoroutine(limitTimeCoroutine);
            limitTimeCoroutine = null;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturned)
        {
            return;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            ReturnPool();
        }
    }
    
}
