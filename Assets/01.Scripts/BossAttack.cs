using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] GameObject bullet;


    void Start()
    {
        //StartCoroutine(SectorPattern());
        StartCoroutine(CirclePattern());
    }

    
    
    // 부채꼴
    IEnumerator SectorPattern()
    {
        int bulletCount = 3;
        float angle = 60f;

        float subAngle = -angle * 0.5f;

        // interval 총알사이 각도
        float interval = angle / (bulletCount - 1);

        for(int i = 0; i< bulletCount; i++)
        {
            float a = subAngle + interval * i;

            Vector2 direction = Quaternion.Euler(0, 0, a)*Vector2.left;

            GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
            b.GetComponent<EnemyBullet>().SetDir(direction);
        }

        yield return null;
    }

    // 원형 발사

    IEnumerator CirclePattern()
    {
        int bulletCount = 30;
        float interval = 360f / bulletCount;

        for(int i =0;  i< bulletCount; i++)
        {
            float angle = interval * i;
            Vector2 direction = Quaternion.Euler(0f,0f,angle)*Vector2.right;
            GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
            b.GetComponent<EnemyBullet>().SetDir(direction);
        }
        yield return null;
    }

}
