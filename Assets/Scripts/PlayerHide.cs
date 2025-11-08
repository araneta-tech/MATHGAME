using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    private bool isHiding = false;
    private bool canHide = false;
    private GameObject currentHideSpot;
    private Vector3 lastPosition;

    private SpriteRenderer spriteRenderer;
    private MonoBehaviour movementScript; // reference to your movement script

    void Start()
    {
        // Find the SpriteRenderer in children
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // Kung ang movement mo ay nasa ibang script (hal. PlayerMovement.cs)
        // palitan mo ang "PlayerMovement" ng exact name ng movement script mo
        movementScript = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (canHide && Input.GetKeyDown(KeyCode.E))
        {
            if (!isHiding)
            {
                // Enter hide mode
                lastPosition = transform.position;
                transform.position = currentHideSpot.transform.position;
                spriteRenderer.enabled = false;
                if (movementScript != null) movementScript.enabled = false; // 🔒 disable movement
                isHiding = true;
            }
            else
            {
                // Exit hide mode
                transform.position = lastPosition;
                spriteRenderer.enabled = true;
                if (movementScript != null) movementScript.enabled = true; // 🔓 enable movement
                isHiding = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HideSpot"))
        {
            canHide = true;
            currentHideSpot = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HideSpot"))
        {
            canHide = false;
            currentHideSpot = null;
        }
    }

    public bool IsHiding()
    {
        return isHiding;
    }
}
