using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth; // ✅ made PUBLIC for UI access

    public float invincibleDuration = 2f;
    private bool isInvincible = false;

    private Renderer playerRenderer;

    [Header("UI")]
    public GameObject gameOverPanel; // ✅ assign sa Inspector!

    void Start()
    {
        currentHealth = maxHealth;
        playerRenderer = GetComponentInChildren<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            ShowGameOver();
        }
        else
        {
            StartCoroutine(Invincibility());
        }
    }

    void ShowGameOver()
    {
        Debug.Log("🟥 GAME OVER!");
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f; // ✅ freeze game
    }

    IEnumerator Invincibility()
    {
        isInvincible = true;

        float elapsed = 0f;
        float flashInterval = 0.15f;

        while (elapsed < invincibleDuration)
        {
            playerRenderer.enabled = !playerRenderer.enabled;
            yield return new WaitForSeconds(flashInterval);
            elapsed += flashInterval;
        }

        playerRenderer.enabled = true;
        isInvincible = false;
    }
}
