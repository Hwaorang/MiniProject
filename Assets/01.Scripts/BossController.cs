using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] int maxHp;
    int nowHp;
    

    void Start()
    {
        nowHp = maxHp;
    }

    public void TakeDamage(int dmg)
    {
        nowHp -= dmg;
        if (nowHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // ¿¬Ãâ
        GameManager.instance.GameClear();
        gameObject.SetActive(false);
               
    }
}
