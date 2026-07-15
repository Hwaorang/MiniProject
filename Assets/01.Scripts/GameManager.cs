using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController playerCon;

    private long score;

    public float currentTime;

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
        nowLife = lifeCount;
        UIManager.instance.SetLifeText(lifeCount.ToString());
        UIManager.instance.SetScoreText(score.ToString());
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
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
            GameOver();
            // 게임 종료
            // 랭킹에 점수 저장
            // 이어하기
        }
    }

    private void ResurrectionPlayer()
    {
        // 플레이어 부활 및 위치 조정, 초기화
        playerCon.transform.position = revivePosition.position;
        playerCon.ResetPlayer();
        UIManager.instance.SetLifeText(nowLife.ToString());

    }

    
    public void GetScore(int s)
    {
        score += s;
        UIManager.instance.SetScoreText(score.ToString());
    }
    
    public void GameClear()
    {
        // 승리 UI
        // 타임스케일 0
        // isPlay false
    }
    public void GameOver()
    {  
        
            // 게임오버 UI뜨게하기  (재시작,로비,게임종료 버튼추가)
            // 타임스케일 0
            // isPlay false  <- isPlay만들어서 추가하기 
        
    }

    public void RegisterPlayer(PlayerController player)
    {
        playerCon = player;
    }

}
