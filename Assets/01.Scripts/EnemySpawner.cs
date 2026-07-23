using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject boss;
    [SerializeField] Transform bossPos;
    
    public List<EnemyController> allEnemy = new List<EnemyController>();    
    public List<EnemyBullet> allEnemyBullet = new List<EnemyBullet>();    
    [SerializeField] List<Rect> spawnArea = new List<Rect>();

    private int minSpawnCount = 1;
    private int maxSpawnCount = 3;
    private float spawnInterval = 3f;

    private int waveSpawnCount = 10;
    private float waveInterval = 15f;

    

    bool isBossSpawn = false;

    void Start()
    {        
        StartCoroutine(SpawnEnemy());
        StartCoroutine(WaveSpawnEnemy());
    }

    private void Update()
    {
        if(!isBossSpawn && GameManager.instance.currentTime>30f)
        {
            isBossSpawn = true;
            SpawnBoss();
        }
    }

    IEnumerator SpawnEnemy()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnInterval);
        while(true)
        {            
            int spawnCount =Random.Range(minSpawnCount,maxSpawnCount);
            SummonEnemy(spawnCount);
            yield return wait;
        }
    }

    IEnumerator WaveSpawnEnemy()
    {
        WaitForSeconds waveWait = new WaitForSeconds(waveInterval);
        while(true)
        {
            yield return waveWait;
            if (isBossSpawn)
            {
                break;
            }
            SummonEnemy(waveSpawnCount);
        }
    }

    void SummonEnemy(int count)
    {
        for(int i = 0; i<count; i++)
        {
            Rect spawnRect = spawnArea[Random.Range(0, spawnArea.Count)];

            Vector2 randPos = new Vector2(Random.Range(spawnRect.xMin, spawnRect.xMax),
                Random.Range(spawnRect.yMin, spawnRect.yMax));

            GameObject enemy = ObjectPoolManager.instance.GetObject("Enemy");

            if (enemy != null)
            {
                enemy.transform.position = randPos;
                enemy.SetActive(true);
                EnemyController enemyController = enemy.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    allEnemy.Add(enemyController);
                }
            }
        } 
    }

    public void SpawnBoss()
    {
        StopAllCoroutines();

        for (int i = 0; i < allEnemy.Count; i++)
        {
            if(allEnemy[i] != null && allEnemy[i].gameObject.activeSelf)
            {
                //allEnemy[i].gameObject.SetActive(false);
                ObjectPoolManager.instance.ReturnObject("Enemy", allEnemy[i].gameObject);
            }
        }
        allEnemy.Clear();

        for(int i = 0; i < allEnemyBullet.Count; i++)
        {
            if (allEnemyBullet[i] != null && allEnemyBullet[i].gameObject.activeSelf)
            {                
                ObjectPoolManager.instance.ReturnObject("EnemyBullet", allEnemy[i].gameObject);
            }
        }
        allEnemyBullet.Clear();


        Instantiate(boss, bossPos.position,Quaternion.identity);
    }


    private void OnDrawGizmosSelected()
    {
        if (spawnArea == null)
        {
            return;
        }
        

        foreach (var area in spawnArea)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);

            Gizmos.DrawCube(center, size);
        }
    }

}
