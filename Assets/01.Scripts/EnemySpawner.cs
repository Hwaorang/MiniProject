using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject boss;
    [SerializeField] Transform bossPos;
    // 모든 적을 집어넣을 리스트
    public List<EnemyController> allEnemy = new List<EnemyController>();
    // 모든 적 총알을 집어넣을 리스트
    public List<EnemyBullet> allEnemyBullet = new List<EnemyBullet>();

    //[SerializeField] List<Transform> spawnPosition = new List<Transform>();
    [SerializeField] List<Rect> spawnArea = new List<Rect>();
    //[SerializeField] Color color = new Color(1, 0, 0, 0.5f);
    

    bool isBossSpawn = false;
    void Start()
    {        
        StartCoroutine(SpawnEnemy());
    }

    private void Update()
    {
        if(!isBossSpawn && GameManager.instance.currentTime>10f)
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
            //int rand = Random.Range(0, spawnPosition.Count);

            //Vector3 targetPos = spawnPosition[rand].position;

            //GameObject enemy = ObjectPoolManager.instance.GetObject("Enemy");
            //enemy.transform.position = targetPos;

            //allEnemy.Add(enemy.GetComponent<EnemyController>());
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
