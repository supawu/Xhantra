using UnityEngine;

public class BossLocomotion : MonoBehaviour
{
    public Transform player; // Assign the player transform in the Inspector
    public float movementSpeed = 3f;
    public float rotationSpeed = 10f;

    private BossManager bossManager;
    private BossAnimationManager animationManager;
    private Rigidbody rigidbody;

    private void Awake()
    {
        bossManager = GetComponent<BossManager>();
        animationManager = GetComponentInChildren<BossAnimationManager>();
        rigidbody = GetComponent<Rigidbody>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        if (player == null) return;

        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if (bossManager.isInteracting) return;

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        // Move towards the player
        transform.position += direction * movementSpeed * Time.deltaTime;

        // Update Animator values
        float vertical = Vector3.Dot(transform.forward, direction);
        float horizontal = Vector3.Dot(transform.right, direction);

        animationManager.UpdateAnimatorValues(vertical, horizontal);
    }

    private void HandleRotation()
    {
        if (bossManager.isInteracting) return;

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        if (direction == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}