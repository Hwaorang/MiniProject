using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    Button retryBtn;
    
    void Start()
    {
        retryBtn = GetComponent<Button>();
        retryBtn.onClick.AddListener(ClickExitBtn);

    }

    void ClickExitBtn()
    {
        GameManager.instance.Exit();
    }
}
