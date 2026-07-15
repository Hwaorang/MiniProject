using System.Collections;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    float moveSpeed = 5f;
    float lifeTime = 3f;

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
        ObjectPoolManager.instance.ReturnObject("WeaponItem", this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer ==LayerMask.NameToLayer("Player"))
        {
            collision.GetComponent<PlayerAttack>().WeaponLevelUp();
            ReturnPool();
        }
    }
}
