using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public float groundCheckDistance = 0.1f; // Distance to check for ground
    public LayerMask groundLayer; // Layer for ground objects
    public float gravity = -9.81f; // Gravity force
    public float groundOffset = 0.1f; // Small offset to keep the Boss grounded

    private Rigidbody rb;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckGround();
        ApplyGravity();
    }

    private void CheckGround()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, groundLayer);

        if (isGrounded)
        {
            // Adjust position to stay grounded
            Vector3 newPosition = hit.point + Vector3.up * groundOffset;
            transform.position = newPosition;
        }
    }

    private void ApplyGravity()
    {
        if (!isGrounded)
        {
            rb.velocity += Vector3.up * gravity * Time.deltaTime;
        }
        else
        {
            // Stop falling when grounded
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
    }
}