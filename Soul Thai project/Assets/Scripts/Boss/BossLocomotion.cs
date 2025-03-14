using UnityEngine;

public class BossLocomotion : MonoBehaviour
{
    public Transform player;
    public float movementSpeed = 3f;
    public float rotationSpeed = 10f;

    private BossManager bossManager;
    private BossAnimationManager animationManager;
    private Rigidbody rb;

    private void Awake()
    {
        bossManager = GetComponent<BossManager>();
        animationManager = GetComponentInChildren<BossAnimationManager>();
        rb = GetComponent<Rigidbody>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        if (bossManager.isDead || bossManager.isInteracting) return;

        if (player == null) return;

        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if (animationManager.anim.GetBool("isInteract"))
        {
            StopMovement(); // Stop movement when attacking
            return;
        }

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        transform.position += direction * movementSpeed * Time.deltaTime;

        float vertical = Vector3.Dot(transform.forward, direction);
        float horizontal = Vector3.Dot(transform.right, direction);

        animationManager.UpdateAnimatorValues(vertical, horizontal);
    }

    private void HandleRotation()
    {
        if (!animationManager.canRotate) return; // Stop rotation during attack

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        if (direction == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // New function to stop player movement
    public void StopMovement()
    {
        if (rb != null)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0); // Preserve vertical velocity for gravity
        }
    }
}
