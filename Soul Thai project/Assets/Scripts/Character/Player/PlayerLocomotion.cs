
using UnityEngine;

public class Locomotion : MonoBehaviour
{
    PlayerManager playerManager;
    Transform cameraObject;
    InputManager inputManager;
    Vector3 moveDirection;
    [HideInInspector]
    public Transform myTransform;
    [HideInInspector]
    public AnimationManager animationManager;

    public new Rigidbody rigidbody;
    public GameObject normalCamera; //non lock camera

    [Header("Movement Stats")]
    [SerializeField]
    float movementSpeed = 5;
    [SerializeField]
    float rotationSpeed = 10;
    [SerializeField] float rollForce = 1.5f; // Direct rolling speed instead of multiplier
    [SerializeField] float rollDuration = 0.15f;
    private bool isRolling = false;
    private float rollTimeRemaining = 0f;
    private Vector3 rollDirection;

    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        rigidbody = GetComponent<Rigidbody>();
        inputManager = GetComponent<InputManager>();
        animationManager = GetComponentInChildren<AnimationManager>();
        cameraObject = Camera.main.transform;
        myTransform = transform;
        animationManager.Initialize();

    }
    void Update()
    {
        if (isRolling)
        {
            rollTimeRemaining -= Time.deltaTime;
            if (rollTimeRemaining <= 0)
            {
                isRolling = false;
                animationManager.anim.SetBool("Rolling", false);
                //animationManager.anim.applyRootMotion = true; // Re-enable root motion after rolling
                //Debug.Log("Roll ended. Root motion re-enabled.");
                //rigidbody.linearVelocity = Vector3.zero; // Reset velocity to prevent sticking

                Debug.Log("Roll ended");
            }
        }
    }



    #region Movement
    Vector3 normalVector;
    Vector3 targetPosition;

    private void HandleRotation(float delta)
    {
        Vector3 targetDir = Vector3.zero;
        float moveOverride = inputManager.moveAmount;

        targetDir = cameraObject.forward * inputManager.vertical;
        targetDir += cameraObject.right * inputManager.horizontal;

        targetDir.Normalize();
        targetDir.y = 0;



        if (targetDir == Vector3.zero)
            targetDir = myTransform.forward;

        float rs = rotationSpeed;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

        myTransform.rotation = targetRotation;
    }

    public void HandleMovement(float delta)

    {
        if (animationManager.anim.GetBool("isInteract"))
        {
            // Only update animation values but don't apply movement
            animationManager.UpdateAnimatorValues(0, 0);
            return;
        }
        if (!isRolling)
        {
            moveDirection = cameraObject.forward * inputManager.vertical;
            moveDirection += cameraObject.right * inputManager.horizontal;
            moveDirection.Normalize();
            moveDirection.y = 0;

            moveDirection *= movementSpeed;

            Vector3 normalVector = Vector3.up;
            Vector3 projectVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);

            animationManager.UpdateAnimatorValues(inputManager.moveAmount, 0);

            rigidbody.linearVelocity = new Vector3(projectVelocity.x, rigidbody.linearVelocity.y, projectVelocity.z);

            if (animationManager.canRotate)
            {
                HandleRotation(delta);
            }
        }
        else
        {
            // During rolling, just update animation values
            animationManager.UpdateAnimatorValues(1, 0);
        }
    }

    public void HandleRolling(float delta)
    {
        if (inputManager.rollFlag && !isRolling)
        {
            // Only roll if we're moving and not already rolling
            if (inputManager.moveAmount > 0 && !animationManager.anim.GetBool("isInteract"))
            {
                // Disable root motion during the roll
                animationManager.anim.applyRootMotion = false;

                // Get the direction to roll
                rollDirection = cameraObject.forward * inputManager.vertical;
                rollDirection += cameraObject.right * inputManager.horizontal;
                rollDirection.Normalize();

                // Set up the roll
                isRolling = true;
                rollTimeRemaining = rollDuration;

               

                // Play animation
                animationManager.PlayTargetAnimation("Rolling", true);
                animationManager.anim.SetBool("Rolling", true);

                // Initial rotation towards roll direction
                if (rollDirection != Vector3.zero)
                {
                    Quaternion rollRotation = Quaternion.LookRotation(rollDirection);
                    myTransform.rotation = rollRotation;
                }

                // Apply a strong impulse force in the roll direction
                rigidbody.linearVelocity = Vector3.zero; // Clear existing velocity
                rigidbody.AddForce(rollDirection * rollForce, ForceMode.Impulse);

                Debug.Log("Starting roll with force: " + rollForce);
            }


            inputManager.rollFlag = false;
        }
    }


    #endregion
}

