using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] int maxHp = 3;
    [SerializeField] int nowHp;

    [SerializeField] GameObject bullet;
    [SerializeField] GameObject item;

    float attackDelay;

    Collider2D collider;

    GameObject player;

    float moveDistance;
    float moveSpeed;
    Vector3 targetPos;
    
    void Start()
    {
        collider = GetComponent<Collider2D>();
        nowHp = maxHp;
        attackDelay = 3f;
        moveDistance = 5f;
        moveSpeed = 3f;
        targetPos = transform.position + Vector3.left * moveDistance;

        player = GameObject.Find("Player");

        StartCoroutine(ShootBullet());
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos,
            moveSpeed * Time.deltaTime);
    }


    public void TakeDamage(int dmg)
    {
        nowHp -= dmg;
        if(nowHp <=0)
        {
            Die();
        }
    }

    void Die()
    {
        // 파괴
        // 파괴 연출
        // 아이템 생성

        Instantiate(item, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }


    IEnumerator ShootBullet()
    {
        WaitForSeconds wait = new WaitForSeconds(attackDelay);

        while(true)
        {
            yield return wait;

            GameObject enemyBullet = Instantiate(bullet, transform.position, Quaternion.identity);

            Vector2 dir = player.transform.position - transform.position;      // 플레이어 위치 - 내 위치

            enemyBullet.GetComponent<EnemyBullet>().SetDir(dir);
        }       

    }
}
