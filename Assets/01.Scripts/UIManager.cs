using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI lifeText;
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
        SetLifeText(GameManager.instance.lifeCount.ToString());
        SetScoreText(GameManager.instance.score.ToString());
    }

    public void SetScoreText(string text)
    {
        scoreText.text = text;
    }
    public void SetLifeText(string text)
    {
        lifeText.text = text;
    }
    public void SetBombText(string text)
    {
        bombText.text = text;
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
