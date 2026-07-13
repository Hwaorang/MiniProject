using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] int maxHp = 3;
    [SerializeField] int nowHp;

    [SerializeField] GameObject bullet;

    float attackDelay;

    Collider2D collider;

    GameObject player;
    
    void Start()
    {
        collider = GetComponent<Collider2D>();
        nowHp = maxHp;
        attackDelay = 3f;
        player = GameObject.Find("Player");

        StartCoroutine(ShootBullet());
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
