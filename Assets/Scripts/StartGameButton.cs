using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public float delay = 2f; // time in seconds before loading next scene

    public void OnStartButtonClicked()
    {
        // Start the coroutine to delay the scene loading
        StartCoroutine(LoadGameAfterDelay());
    }

    private System.Collections.IEnumerator LoadGameAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GameScene"); // name of your actual game scene
    }
}
