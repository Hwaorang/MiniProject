using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    
    public void MoveGameScene()
    {
        SceneManager.LoadScene("GameScene");
        GameManager.instance.ResetGameData();
    }
}
