using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] GameObject bullet;


    void Start()
    {        
        StartCoroutine(BossPatternLoop());
    }

    

    IEnumerator BossPatternLoop()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            int randomPattern = Random.Range(0, 4);

            switch(randomPattern)
            {
                case 0:
                    yield return StartCoroutine(CirclePattern());
                    break;
                case 1:
                    yield return StartCoroutine(SectorPattern());
                    break;
                case 2:
                    yield return StartCoroutine(TargetPattern(5, 0.2f));
                    break;
                case 3:
                    yield return StartCoroutine(SpiralPattern());
                    break;
            }
            yield return new WaitForSeconds(3.1f);
        }

        
    }


    
    // 부채꼴
    IEnumerator SectorPattern()
    {
        int bulletCount = 10;
        float angle = 75f;
        float bulletSpeed = 11f;

        float subAngle = -angle * 0.5f;

        // interval 총알사이 각도
        float interval = angle / (bulletCount - 1);

        for(int i = 0; i< bulletCount; i++)
        {
            float a = subAngle + interval * i;

            Vector2 direction = Quaternion.Euler(0, 0, a)*Vector2.left*bulletSpeed;

            GameObject enemyBullet = ObjectPoolManager.instance.GetObject("EnemyBullet");
            enemyBullet.transform.position = transform.position;
            enemyBullet.SetActive(true);
            enemyBullet.GetComponent<EnemyBullet>().SetDir(direction);
        }

        yield return null;
    }

    // 원형 발사

    IEnumerator CirclePattern()
    {
        int bulletCount = 40;
        float interval = 360f / bulletCount;
        float bulletSpeed = 12f;

        for(int i =0;  i< bulletCount; i++)
        {
            float angle = interval * i;
            Vector2 direction = Quaternion.Euler(0f,0f,angle)*Vector2.right *bulletSpeed;
            GameObject enemyBullet = ObjectPoolManager.instance.GetObject("EnemyBullet");
            enemyBullet.transform.position = transform.position;
            enemyBullet.SetActive(true);
            enemyBullet.GetComponent<EnemyBullet>().SetDir(direction);
        }
        yield return null;
    }

    IEnumerator TargetPattern(int count, float delay)
    {
        Transform target = GameManager.instance.playerCon.transform;

        for(int i = 0; i<count; i++)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float bulletSpeed = 15f;
            GameObject enemyBullet = ObjectPoolManager.instance.GetObject("EnemyBullet");
            enemyBullet.transform.position = transform.position;
            enemyBullet.SetActive(true);
            enemyBullet.GetComponent<EnemyBullet>().SetDir(direction*bulletSpeed);

            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator SpiralPattern()
    {
        int bulletCount = 60;
        float bulletSpeed = 12f;
        float angleStep = 10f;
        float currentAngle = 0f;

        for(int i = 0; i<bulletCount;i++)
        {
            Vector2 direction = Quaternion.Euler(0, 0, currentAngle) * Vector2.up;

            GameObject enemyBullet = ObjectPoolManager.instance.GetObject("EnemyBullet");
            enemyBullet.transform.position = transform.position;
            enemyBullet.SetActive(true);
            enemyBullet.GetComponent<EnemyBullet>().SetDir(direction * bulletSpeed);

            currentAngle += angleStep;

            yield return new WaitForSeconds(0.05f);
        }
    }
}
