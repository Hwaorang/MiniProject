using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] TextMeshProUGUI scoreText;    
    [SerializeField] TextMeshProUGUI bombText;
    
    [SerializeField] Image gameOverImg;
    [SerializeField] Image gameClearImg;

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
        
        gameOverImg.gameObject.SetActive(false);
        gameClearImg.gameObject.SetActive(false);
    }

    private void Start()
    {           
        SetScoreText(GameManager.instance.score);
    }

    public void SetScoreText(int currentScore)
    {
        scoreText.text = $"Score : {currentScore}";
    }
    
    public void SetBombText(int currentBomb)
    {
        bombText.text = $"Bomb : {currentBomb}";
    }

    public void SetGameClearUI()
    {
        gameClearImg.gameObject.SetActive(true);
    }
    public void SetGameOverUI()
    {
        gameOverImg.gameObject.SetActive(true);
    }
}
