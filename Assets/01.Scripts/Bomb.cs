using UnityEngine;

public class Bomb : MonoBehaviour
{
    int damage = 5;

    Camera cam;
    Collider2D[] col;

    
    void Start()
    {
        cam = Camera.main;
    }

    public void UseBomb()
    {
       

        Vector3 min = cam.ViewportToWorldPoint(Vector3.zero);
        Vector3 max = cam.ViewportToWorldPoint(Vector3.one);

        col = Physics2D.OverlapAreaAll(min, max);

        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int enemyBulletLayer = LayerMask.NameToLayer("EnemyBullet");
        int bossLayer = LayerMask.NameToLayer("Boss");

        foreach(Collider2D col2 in col)
        {
            if (col2 == null)
            {
                continue;
            }

            if (col2.gameObject.layer == enemyLayer)
            {
                EnemyController enemy = col2.gameObject.GetComponent<EnemyController>();
                if(enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
            else if (col2.gameObject.layer == enemyBulletLayer)
            {
                col2.gameObject.SetActive(false);
            }
            else if (col2.gameObject.layer == bossLayer)
            {
                BossController boss = col2.gameObject.GetComponent<BossController>();
                if(boss != null)
                {
                    boss.TakeDamage(damage);
                }
            }
        }
    }
}
