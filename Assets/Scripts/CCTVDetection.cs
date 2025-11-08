using UnityEngine;

public class CCTVDetection : MonoBehaviour
{
    public EnemyAI assignedEnemy; // Enemy guarding this CCTV
    public Transform player;
    public Transform cctvSpot; // ✅ Enemy will investigate this exact position
    public float detectionRange = 12f;
    public float viewAngle = 60f;

    private bool playerDetected = false;

    void Update()
    {
        if (player == null || assignedEnemy == null) return;

        if (CanSeePlayer())
        {
            if (!playerDetected)
            {
                playerDetected = true;
                AlertEnemy();
            }
        }
        else
        {
            if (playerDetected)
                Debug.Log("✅ CCTV: Player lost from view by " + gameObject.name);

            playerDetected = false;
        }
    }

    bool CanSeePlayer()
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dirToPlayer);
        float dist = Vector3.Distance(transform.position, player.position);

        if (angle > viewAngle || dist > detectionRange) return false;

        if (Physics.Raycast(transform.position, dirToPlayer, out RaycastHit hit, detectionRange))
        {
            return hit.transform == player;
        }

        return false;
    }

    void AlertEnemy()
    {
        Debug.Log("🚨 CCTV: Player detected by " + gameObject.name);

        assignedEnemy.playerInSight = true;

        // ✅ Investigate CCTV location instead of player
        if (cctvSpot != null)
            assignedEnemy.InvestigatePosition(cctvSpot.position);
        else
            assignedEnemy.InvestigatePosition(transform.position);

        // ✅ Make enemy look towards CCTV
        Vector3 lookDir = (transform.position - assignedEnemy.transform.position).normalized;
        assignedEnemy.transform.forward = lookDir;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Vector3 dir1 = Quaternion.Euler(0, viewAngle, 0) * transform.forward;
        Vector3 dir2 = Quaternion.Euler(0, -viewAngle, 0) * transform.forward;

        Gizmos.DrawRay(transform.position, dir1 * detectionRange);
        Gizmos.DrawRay(transform.position, dir2 * detectionRange);
    }
}
