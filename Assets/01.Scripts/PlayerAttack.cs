using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform firePosition;
    [SerializeField] GameObject bullet;

    int bombCount = 3;

    Bomb bomb;

    public int level = 0;

    float attackDelay = 0.15f;
    private float nextAttackTime;

    private float bombDelay = 0.5f;
    private float nextBombTime;



    private Vector3 offset;

    private void Start()
    {
        nextAttackTime = Time.time;
        nextBombTime = Time.time;
        bomb = GetComponentInChildren<Bomb>();
        offset = new Vector3(0, 0.3f);

        UIManager.instance.SetBombText(bombCount);
    }


    void Update()
    {
        if(GameManager.instance.isPlay)
        {
            if (Keyboard.current.zKey.wasPressedThisFrame)
            {
                Fire(level);
            }

            if (Keyboard.current.xKey.wasPressedThisFrame)
            {
                TryUseBomb();
            }
        }
        
    }

    void Fire(int level = 0)
    {
        
        if (Time.time - nextAttackTime < attackDelay)
        {
            return;
        }

        int bulletCount = level + 1;
        float bulletY = firePosition.position.y - ((bulletCount - 1) * offset.y / 2f);

        for(int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = ObjectPoolManager.instance.GetObject("Bullet");

            if(bullet != null )
            {
                float targetY = bulletY + (i * offset.y);

                bullet.transform.position = new Vector3(firePosition.position.x, targetY, firePosition.position.z);

                bullet.SetActive(true);
            }
        }
        nextAttackTime = Time.time + attackDelay;
    }


    void TryUseBomb()
    {
        if (Time.time - nextBombTime>bombDelay && bombCount > 0)
        {
            bombCount--;
            bomb.UseBomb();
            UIManager.instance.SetBombText(bombCount);

        }
        else
        {
            return;
        }
        nextBombTime = Time.time + bombDelay;
    }

    public void WeaponLevelUp()
    {
        level++;

        level = Mathf.Clamp(level, 0, 2); // ()범위 안에 존재해야한다.
    }

    public void GetBomb()
    {
        bombCount++;
        UIManager.instance.SetBombText(bombCount);
    }
    public void ResetAttack()
    {
        level = 0;
        bombCount = 3;

        UIManager.instance.SetBombText(bombCount);
    }
}
