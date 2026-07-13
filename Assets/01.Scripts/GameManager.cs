using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    PlayerController player;

    private long score;

    [SerializeField] Transform revivePosition;

    [SerializeField] int lifeCount = 3;
    [SerializeField] int bombCount = 3;
    int nowLife;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }



    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        nowLife = lifeCount;
    }

    public void ReducePlayerLife()
    {
        if(nowLife >0)
        {
            nowLife--;
            ResurrectionPlayer();
            // 플레이어 부활
            // 플레이어 초기화(무기레벨 등)
            // 플레이어 잠시 무적 처리
        }
        else
        {
            // 게임 종료
            // 랭킹에 점수 저장
            // 이어하기
        }
    }

    private void ResurrectionPlayer()
    {
        // 플레이어 부활 및 위치 조정, 초기화
        player.transform.position = revivePosition.position;
        player.ResetPlayer();
        bombCount = 3;
    }

    public void TryUseBomb()
    {
        if(bombCount>0)
        {
            bombCount--;
            // 실제 폭탄 효과 구현 부분
        }
        else
        {
            // 경고 메시지 or 아무 변화 없음
        }
    }

}
