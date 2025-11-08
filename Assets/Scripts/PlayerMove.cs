using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Animator animator;
    private bool facingRight = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0f, moveZ).normalized;

        // Move the player
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

        // Flip horizontally based on movement direction
        HandleFlip(moveX);

        // Update animation
        UpdateAnimation(move);

    }

    void HandleFlip(float moveX)
    {
        if (moveX > 0 && !facingRight)
        {
            Flip(); // Now flip when moving right and already facing right
        }
        else if (moveX < 0 && facingRight)
        {
            Flip(); // Flip when moving left and already facing left
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Flip the X axis
        transform.localScale = scale;
    }

    void UpdateAnimation(Vector3 move)
    {
        bool isMoving = move.magnitude > 0.1f;
        animator.SetBool("isWalking", isMoving);

        if (isMoving)
        {
            animator.SetFloat("InputX", move.x);
            animator.SetFloat("InputZ", move.z);
        }
    }
}