using UnityEngine;

public class Bomb : MonoBehaviour
{
    

    [SerializeField] int damage = 5;
    [SerializeField] float delayTime;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask enemyBulletLayer;    



    void Start()
    {
        
    }

    public void UseBomb()
    {
        // 1. 모든 적 데미지        
        // 1-2 '모든 적 리스트'를 만들어서 그 리스트에 데미지 주기 

        foreach(EnemyController enemy in EnemySpawner.instance.allEnemy)
        {
            enemy.TakeDamage(damage);
        }



        // 2. 모든 적 총알 파괴
        // 모든 적 총알 리스트를 만들어서 그 리스트 전부 파괴
        foreach (EnemyBullet bullet in EnemySpawner.instance.allEnemyBullet)
        {
            Destroy(bullet);
        }
        EnemySpawner.instance.allEnemyBullet.Clear();
    }

}
