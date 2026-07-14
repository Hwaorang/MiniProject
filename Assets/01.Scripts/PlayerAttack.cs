using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform firePosition;
    [SerializeField] GameObject bullet;
    
    [SerializeField] float attackDelay;
    [SerializeField] public int level;

    private float nextAttackTime;

    private Vector3 offset;

    private void Start()
    {
        nextAttackTime = Time.time;
        offset = new Vector3(0, 0.2f);
    }


    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame
            && Time.time>=nextAttackTime /*공격 딜레이가 지났는가*/)
        {
            Fire(level); // 공격
        }
    }

    void Fire(int level = 0)
    {
        // 레벨에따른 총알생성
        // 공격다시 사용가능한 시간설정
        switch(level)
        {
            case 0:
            {
                    Instantiate(bullet, firePosition.position, Quaternion.identity);
                    return;
            }
            case 1:
            {
                    Instantiate(bullet, firePosition.position+offset, Quaternion.identity);
                    Instantiate(bullet, firePosition.position-offset, Quaternion.identity);
                    return;
            }
            case 2:
            {
                    Instantiate(bullet, firePosition.position, Quaternion.identity);
                    Instantiate(bullet, firePosition.position+offset, Quaternion.identity);
                    Instantiate(bullet, firePosition.position-offset, Quaternion.identity);
                    return;
            }

        }
        

        nextAttackTime = Time.time + attackDelay;
    }

    public void WeaponLevelUp()
    {
        level++;

        level = Mathf.Clamp(level, 0, 2); // ()범위 안에 존재해야한다.
    }
}
