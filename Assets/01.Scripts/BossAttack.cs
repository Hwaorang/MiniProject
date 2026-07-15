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
            int randomPattern = Random.Range(0, 2);

            switch(randomPattern)
            {
                case 0:
                    yield return StartCoroutine(CirclePattern());
                    break;
                case 1:
                    yield return StartCoroutine(SectorPattern());
                    break;
            }
            yield return new WaitForSeconds(3.1f);
        }

        
    }


    
    // 부채꼴
    IEnumerator SectorPattern()
    {
        int bulletCount = 10;
        float angle = 65f;
        float bulletSpeed = 8;

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
        float bulletSpeed = 8;

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

}
