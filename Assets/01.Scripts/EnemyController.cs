using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] int maxHp = 3;
    [SerializeField] int nowHp;

    [SerializeField] GameObject bullet;    

    [SerializeField] List<DropItem> dropItemList = new List<DropItem>();

    float attackRange;
    float attackDelay;

    Collider2D collider;
    PlayerController player;

    bool canAttack = false;
    float moveSpeed;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        
        nowHp = maxHp;
        attackDelay = 3f;
        attackRange = 12f;
        moveSpeed = 3f;        
        StartCoroutine(ShootBullet());        
        player = null;
    }    

    private void Update()
    {        
        if(player == null)
        {
            player = GameManager.instance.playerCon;
        }
        CheckDistance();
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance < attackRange)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
            Move();
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position,player.transform.position,moveSpeed * Time.deltaTime);
    }

    Vector2 GetDirecition()
    {
        return player.transform.position - transform.position;
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
        GameManager.instance.GetScore(1);
        
        DropItems();        

        ReturnPool();
    }

    void DropItems()
    {
        float random = Random.Range(0f, 100f);
        float currentSum = 0f;

        foreach(DropItem item in dropItemList)
        {
            currentSum += item.dropPro;

            if(random <= currentSum)
            {
                GameObject dropItem = ObjectPoolManager.instance.GetObject(item.itemObj.name);

                dropItem.transform.position = transform.position;
                break;
            }
        }
    }


    void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject("Enemy", this.gameObject);
    }



    IEnumerator ShootBullet()
    {
        WaitForSeconds wait = new WaitForSeconds(attackDelay);
        
        while(true)
        {
            if(canAttack)
            {                
                GameObject enemyBullet = ObjectPoolManager.instance.GetObject("EnemyBullet");

                if(enemyBullet != null)
                {
                    enemyBullet.transform.position = transform.position;                         
                    Vector2 dir = GetDirecition();
                    //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    //enemyBullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                    enemyBullet.SetActive(true);

                    enemyBullet.GetComponent<EnemyBullet>().SetDir(dir);
                }
            }            
            yield return wait;
        }       

    }
}
