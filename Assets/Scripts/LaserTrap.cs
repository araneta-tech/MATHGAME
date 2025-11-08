using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    public EnemyAI assignedEnemy; // Enemy that responds to this laser

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Laser triggered – Enemy Investigating!");
            assignedEnemy.InvestigatePosition(transform.position);
        }
    }
}
