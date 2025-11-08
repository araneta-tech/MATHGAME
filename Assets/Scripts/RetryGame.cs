using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryGame : MonoBehaviour
{
    public void Retry()
    {
        Time.timeScale = 1f; // ✅ ibalik normal game speed
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ✅ reload current scene
        Debug.Log("🔄 Retry pressed - Reloading level!");
    }
}
