using System.Collections;             // <- kailangan para sa IEnumerator
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    public float invincibleDuration = 2f;
    private bool isInvincible = false;

    private Renderer playerRenderer;

    void Start()
    {
        currentHealth = maxHealth;

        // Hanapin ang Renderer nang maayos (sprite o mesh)
        playerRenderer = GetComponent<Renderer>();
        if (playerRenderer == null)
        {
            // kung walang Renderer sa parent, subukan hanapin sa children (SpriteRenderer o MeshRenderer)
            playerRenderer = GetComponentInChildren<Renderer>();
        }

        if (playerRenderer == null)
        {
            Debug.LogWarning("PlayerHealth: No Renderer found on player or children. Blinking won't work.");
        }
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
            Die();
        }
        else
        {
            StartCoroutine(Invincibility());
        }
    }

    IEnumerator Invincibility()
    {
        isInvincible = true;

        if (playerRenderer != null)
        {
            float elapsed = 0f;
            float flashInterval = 0.15f; // faster blinking
            while (elapsed < invincibleDuration)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                yield return new WaitForSeconds(flashInterval);
                elapsed += flashInterval;
            }
            playerRenderer.enabled = true;
        }
        else
        {
            // fallback: just wait
            yield return new WaitForSeconds(invincibleDuration);
        }

        isInvincible = false;
    }

    void Die()
    {
        Debug.Log("PLAYER DIED - Restarting...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
