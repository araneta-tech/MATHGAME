using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth; // Reference sa player health
    public Image[] hearts; // 5 heart images

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth.currentHealth)
                hearts[i].enabled = true; // show heart
            else
                hearts[i].enabled = false; // hide heart
        }
    }
}
