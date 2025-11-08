using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Image fadeImage;          // UI Image (black overlay)
    public GameObject gameOverUI;    // Panel for Game Over
    public float fadeDuration = 1.5f;

    private bool isGameOver = false;

    public void TriggerGameOver()
    {
        if (!isGameOver)
            StartCoroutine(GameOverSequence());
    }

    IEnumerator GameOverSequence()
    {
        isGameOver = true;

        
        float t = 0;
        Color color = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        
        gameOverUI.SetActive(true);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
