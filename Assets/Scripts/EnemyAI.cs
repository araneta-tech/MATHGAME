using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    private PlayerHide playerHide;

    [Header("Movement Settings")]
    public float chaseSpeed = 3f;
    public float returnSpeed = 2f;
    public float detectionAngle = 45f;
    public float detectionRange = 10f;
    public float closeDetectionRange = 2f;

    private Vector3 originalPosition;
    private Quaternion originalRotation; // save the start facing

    [HideInInspector]
    public bool playerInSight = false;

    private bool isChasing = false;
    private bool isInvestigating = false;

    [Header("Investigation Settings")]
    public float investigateDuration = 3f;
    private float investigateTimer = 0f;
    private Vector3 investigateTarget;

    void Start()
    {
        if (player != null)
            playerHide = player.GetComponent<PlayerHide>();

        originalPosition = transform.position;
        originalRotation = transform.rotation; // store initial facing direction
    }

    void Update()
    {
        if (player == null) return;

        bool hiding = playerHide != null && playerHide.IsHiding();

        // Stop chase if player hiding → return to origin
        if (hiding)
        {
            isChasing = false;
            playerInSight = false;
            isInvestigating = false;
            ReturnToOrigin();
            return;
        }

        // Update sight
        playerInSight = CanSeePlayer();

        // State transitions
        if (playerInSight)
        {
            isChasing = true;
            isInvestigating = false;
        }
        else if (isChasing) // Lost sight while chasing
        {
            isChasing = false;
            InvestigatePosition(player.position);
        }

        if (isChasing)
            ChasePlayer();
        else if (isInvestigating)
            InvestigateBehavior();
        else
            ReturnToOrigin();
    }

    bool CanSeePlayer()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        // Close range override: always see player if very close
        if (dist <= closeDetectionRange)
            return true;

        if (dist > detectionRange)
            return false;

        // Angle-based vision
        Vector3 dir = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dir);
        return angle <= detectionAngle;
    }

    void ChasePlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * 5f);
        transform.position += transform.forward * chaseSpeed * Time.deltaTime;
    }

    void InvestigateBehavior()
    {
        Vector3 dir = (investigateTarget - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * 3f);
        transform.position = Vector3.MoveTowards(transform.position, investigateTarget, returnSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, investigateTarget) < 0.5f)
            investigateTimer -= Time.deltaTime;

        if (investigateTimer <= 0f)
        {
            isInvestigating = false;
            playerInSight = false;
        }
    }

    // ✅ Updated ReturnToOrigin() — natural movement + correct facing reset
    void ReturnToOrigin()
    {
        Vector3 dirToOrigin = (originalPosition - transform.position);

        // Habang bumabalik, nakaharap sa direksyon ng paglalakad
        if (dirToOrigin.magnitude > 0.05f)
        {
            Vector3 dir = dirToOrigin.normalized;
            transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * 3f);
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, returnSpeed * Time.deltaTime);
        }
        else
        {
            // Pagdating sa origin, i-snap sa tamang pwesto at harap
            transform.position = originalPosition;
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, Time.deltaTime * 3f);
        }
    }

    public void InvestigatePosition(Vector3 pos)
    {
        investigateTarget = pos;
        investigateTimer = investigateDuration;
        isInvestigating = true;
        isChasing = false;
    }
}
