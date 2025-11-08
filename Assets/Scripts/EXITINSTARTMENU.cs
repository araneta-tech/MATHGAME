using UnityEngine;
using UnityEngine.SceneManagement;

public class EXITINSTARTMENU : MonoBehaviour
{
    public void PlayGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Game Exited"); // For testing in Unity Editor
        Application.Quit();       // This closes the game when built
    }
}
