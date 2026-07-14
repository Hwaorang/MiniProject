using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    PlayerController player;

    private long score;

    [SerializeField] Transform revivePosition;

    [SerializeField] int lifeCount = 3;
    
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
            UIManager.instance.SetLifeText(nowLife.ToString());
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
        UIManager.instance.SetLifeText(nowLife.ToString());

    }

    
    public void GetScore(int s)
    {
        score += s;
        UIManager.instance.SetScoreText(score.ToString());
    }

}
