using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🎯 Player reached portal!");
            SceneManager.LoadScene("GameComplete"); // ✅ load next scene
        }
    }
}
