using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    Button exitBtn;
    
    void Start()
    {
        exitBtn = GetComponent<Button>();
        exitBtn.onClick.AddListener(ClickExitBtn);

    }

    void ClickExitBtn()
    {
        GameManager.instance.Exit();
    }
}
