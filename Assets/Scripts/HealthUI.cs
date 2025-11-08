using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth; // Drag Player here
    public Image[] hearts; // Drag 5 heart Image objects here

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = (i < playerHealth.currentHealth);
        }
    }
}
