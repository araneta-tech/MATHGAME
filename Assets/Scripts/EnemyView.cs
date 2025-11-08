using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private EnemyAI parentAI;

    void Start()
    {
        parentAI = GetComponentInParent<EnemyAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parentAI.playerInSight = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parentAI.playerInSight = false;
        }
    }
}
