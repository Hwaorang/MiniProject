using UnityEngine;
using UnityEngine.UI;

public class RetryButton : MonoBehaviour
{
    Button retryBtn;
    void Start()
    {
        retryBtn = GetComponent<Button>();
        retryBtn.onClick.AddListener(ClickRetryBtn);

    }

    void ClickRetryBtn()
    {
        GameManager.instance.Retry();
        GameManager.instance.ResetGameData();
    }

    
}
