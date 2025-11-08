using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage;         // Black overlay image
    public float fadeDuration = 1f; // Duration ng fade
    public string nextSceneName;    // Name ng next scene

    private void Start()
    {
        // Fade in mula sa black pag nag-start ang scene
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(FadeIn());
    }

    public void FadeToNextScene()
    {
        // Tawagin ito sa OnClick ng Start button
        StartCoroutine(FadeOutAndLoad());
    }

    IEnumerator FadeIn()
    {
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = 1f - (t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0;
        fadeImage.color = color;
    }

    IEnumerator FadeOutAndLoad()
    {
        Color color = fadeImage.color;
        float t = 0;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = t / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }

        // Load next scene pagkatapos ng fade
        SceneManager.LoadScene(nextSceneName);
    }
}
