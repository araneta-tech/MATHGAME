using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
   // private Animator animator;

    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0f, moveZ);

        // Move the player
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

        // Update animation
        //UpdateAnimation(move);
    }

    //void UpdateAnimation(Vector3 move)
   // {
       // bool isMoving = move.magnitude > 0.1f;
       // animator.SetBool("isWalking", isMoving);

       // if (isMoving)
       // {
            //animator.SetFloat("InputX", move.x);
            //animator.SetFloat("InputZ", move.z);
        //}
    //}
}
