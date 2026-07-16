using System.Collections;
using UnityEngine;


public enum ItemType
{
    Life,
    Score,
    Power,
    Bomb
}


public class Item : MonoBehaviour
{
    float moveSpeed = 5f;
    float lifeTime = 3f;

    [SerializeField] ItemType type;
    [SerializeField] int itemValue;

    private Coroutine limitTimeCoroutine;    

    private void OnEnable()
    {
        limitTimeCoroutine = StartCoroutine(LimitTime());
    }
    private void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }
    
    IEnumerator LimitTime()
    {
        yield return new WaitForSeconds(lifeTime);
        ReturnPool();
    }

    void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject(this.gameObject.name, this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer ==LayerMask.NameToLayer("Player"))
        {

            ApplyItem(collision.gameObject);
            ReturnPool();
        }
    }

    private void ApplyItem(GameObject player)
    {
        PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();        

        switch (type)
        {
            case ItemType.Life:
                GameManager.instance.GetLife(itemValue);
                break;
            case ItemType.Score:
                GameManager.instance.GetScore(itemValue);
                break;
            case ItemType.Power:
                playerAttack.WeaponLevelUp();
                break;
            case ItemType.Bomb:
                playerAttack.GetBomb();
                break;
                
        }
    }
}
