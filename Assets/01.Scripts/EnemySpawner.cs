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
    
    

    bool isBossSpawn = false;
    void Start()
    {        
        StartCoroutine(SpawnEnemy());
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
        WaitForSeconds wait = new WaitForSeconds(3f);
        while(true)
        {
            //if(GameManager.instance.currentState ==GameState.Playing)
            //{
                
            //}
            SummonEnemy();
            yield return wait;
        }
    }

    void SummonEnemy()
    {
        Rect spawnRect = spawnArea[Random.Range(0, spawnArea.Count)];

        Vector2 randPos = new Vector2(Random.Range(spawnRect.xMin, spawnRect.xMax),
            Random.Range(spawnRect.yMin, spawnRect.yMax));

        GameObject enemy = ObjectPoolManager.instance.GetObject("Enemy");

        if(enemy != null)
        {
            enemy.transform.position = randPos;
            enemy.SetActive(true);
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if(enemyController != null )
            {
                allEnemy.Add(enemyController);
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
                allEnemy[i].gameObject.SetActive(false);
            }
        }
        allEnemy.Clear();

        for(int i = 0; i < allEnemyBullet.Count; i++)
        {
            if (allEnemyBullet[i] != null && allEnemyBullet[i].gameObject.activeSelf)
            {
                allEnemyBullet[i].gameObject.SetActive(false);
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
