using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.SceneManagement;


//public enum GameState
//{
//    Title,
//    Playing,
//    Paused,
//    GameOver,
//    Clear
//}



public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController playerCon;

    public long score;

    public float currentTime;

    //public GameState currentState;

    [SerializeField] Transform revivePosition;

    [SerializeField] public int lifeCount = 3;
    
    int nowLife;

    public bool isPlay = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            ResetGameData();
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        
    }

    //public void ChangeState(GameState state)
    //{
    //    currentState = state;

    //    switch(currentState)
    //    {
    //        case GameState.Title:
    //            Time.timeScale = 1f;
    //            break;
    //        case GameState.Playing:
    //            Time.timeScale = 1f;
    //            break;
    //        case GameState.Paused:
    //            Time.timeScale = 0f;
    //            break;
    //        case GameState.GameOver:
    //            Time.timeScale = 0f;
    //            break;
    //        case GameState.Clear:
    //            Time.timeScale = 0f;
    //            break;
    //    }
    //}

    void Start()
    {        
        
        
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
    }

    public void ResetGameData()
    {
        score = 0;
        nowLife = lifeCount;
        currentTime = 0f;
        isPlay = true;
    }

    public void ReducePlayerLife()
    {
        if(nowLife >0)
        {
            nowLife--;
            ResurrectionPlayer();
            UIManager.instance.SetLifeText(nowLife.ToString());            
        }
        else
        {
            GameOver();            
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
    public void GetLife(int life)
    {
        nowLife += life;
        UIManager.instance.SetLifeText(nowLife.ToString());
    }
    
    public void GameClear()
    {
        // 승리 UI
        UIManager.instance.SetGameClearUI();
        Time.timeScale = 0;
        //타임스케일 0 (currentState = GameState.Clear)
        isPlay = false;
    }
    public void GameOver()
    {
        UIManager.instance.SetGameOverUI();
        Time.timeScale = 0;         
        isPlay = false;

    }

    public void Retry()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
        isPlay = true;
    }
    public void Exit()
    {
        SceneManager.LoadScene("LobbyScene");
        Time.timeScale = 1;        
    }
    public void RegisterPlayer(PlayerController player)
    {
        playerCon = player;
    }

}
