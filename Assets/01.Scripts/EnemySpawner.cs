using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance; // 직접구현할때는 싱글톤 말고 다른걸로 구현하기

    [SerializeField] GameObject enemy;

    // 모든 적을 집어넣을 리스트
    public List<EnemyController> allEnemy = new List<EnemyController>();
    // 모든 적 총알을 집어넣을 리스트
    public List<EnemyBullet> allEnemyBullet = new List<EnemyBullet>();

    [SerializeField] List<Transform> spawnPosition = new List<Transform>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        // 일정시간마다 적 생성
        // 적 이동
        // 보스 소환
        StartCoroutine(SpawnEnemy());


        // 적 소환할 때 마다 allEnemy에 Add한다.
        // 적이 소환 해제/파괴 될 때마다 allEnemy에서 Remove한다.
    }

    IEnumerator SpawnEnemy()
    {
        WaitForSeconds wait = new WaitForSeconds(3f);
        while(true)
        {
            int rand = Random.Range(0, spawnPosition.Count);

            GameObject obj = Instantiate(enemy, spawnPosition[rand].position,Quaternion.identity);
            allEnemy.Add(obj.GetComponent<EnemyController>());
            yield return wait;
        }
    }

    public void ClearEnemyBullet()
    {
        for(int i = 0; i< allEnemy.Count; i++)
        {
            Destroy(allEnemyBullet[i]);            
            i--;
        }       
    }

    public void TakeDamageAllEnemy(int dmg)
    {
        foreach(EnemyController enemy in allEnemy)
        {
            enemy.TakeDamage(dmg);
        }
    }

}
