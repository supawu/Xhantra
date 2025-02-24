
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
    float rollingSpeedMultiplier = 1.7f;

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
        moveDirection = cameraObject.forward * inputManager.vertical;
        moveDirection += cameraObject.right * inputManager.horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0;

       

        float speed = movementSpeed;

        if (animationManager.anim.GetCurrentAnimatorStateInfo(0).IsName("Rolling"))//Increasing Speed when we roll
        {
            speed *= rollingSpeedMultiplier;
        }
        
        moveDirection *= speed;

        Vector3 normalVector = Vector3.up;
        Vector3 projectVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);

        animationManager.UpdateAnimatorValues(inputManager.moveAmount, 0);

        rigidbody.linearVelocity = projectVelocity;

        if (animationManager.canRotate)
        {
            HandleRotation(delta);
        }
    }

    public void HandleRolling(float delta)
    {
        if(animationManager.anim.GetBool("isInteract"))
        {
            return;
        }

        if (inputManager.rollFlag)
        {
            moveDirection = cameraObject.forward * inputManager.vertical;
            moveDirection += cameraObject.right * inputManager.horizontal;

            if(inputManager.moveAmount >0)
            {
                animationManager.PlayTargetAnimation("Rolling",true);
                moveDirection.y =0;
                Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                myTransform.rotation = rollRotation;

                
            }
            else
            {
                animationManager.PlayTargetAnimation("RunBackward",true);
            }
            inputManager.rollFlag =false;
        }
    }

    
    #endregion
}
