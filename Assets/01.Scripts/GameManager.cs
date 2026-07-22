using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController playerCon;

    public int score;

    public float currentTime;    

    [SerializeField] Transform revivePosition;

    [SerializeField] public int lifeCount = 3;
    
    public int nowLife;

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

    private void Update()
    {
        if(isPlay)
        {
            currentTime += Time.deltaTime;
        }
        
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
        }
        else
        {
            GameOver();            
        }
    }

    private void ResurrectionPlayer()
    {        
        playerCon.transform.position = revivePosition.position;
        playerCon.ResetPlayer(); 
    }

    
    public void GetScore(int s)
    {
        score += s;
        UIManager.instance.SetScoreText(score);
    }
    public void GetLife(int life)
    {
        nowLife += life;        
    }
    
    public void GameClear()
    {        
        UIManager.instance.SetGameClearUI();
        Time.timeScale = 0;        
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
