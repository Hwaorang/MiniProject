using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform firePosition;
    [SerializeField] GameObject bullet; // 프리팹으로 만들어서 넣을 예정
    
    [SerializeField] float attackDelay;
    [SerializeField] int level;

    private float nextAttackTime;
    private void Start()
    {
        nextAttackTime = Time.time;
    }


    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame
            && Time.time>=nextAttackTime)// 공격 딜레이가 지났는가
        {
            Fire(); // 공격
        }
    }

    void Fire()
    {
        // 레벨에따른 총알생성
        //공격다시 사용가능한 시간설정

        Instantiate(bullet,firePosition.position,Quaternion.identity);

        nextAttackTime = Time.time + attackDelay;
    }
}
