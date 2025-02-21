
using UnityEngine;

public class Locomotion : MonoBehaviour
{
   Transform cameraObject;
   InputManager inputManager;
   Vector3 moveDirection;
   [HideInInspector]
   public Transform myTransform;
   [HideInInspector]
   public AnimationManager animationManager;

   public new Rigidbody rigidbody;
   public GameObject normalCamera; //non lock camera
   
   [Header("Stats")]
   [SerializeField]
   float movementSpeed = 5;
   [SerializeField]
   float rotationSpeed=10;

   void Start()
   {
    rigidbody = GetComponent<Rigidbody>();
    inputManager = GetComponent<InputManager>();
    animationManager = GetComponentInChildren<AnimationManager>();
    cameraObject = Camera.main.transform;
    myTransform =transform;
    animationManager.Initialize();

   }

   public void Update()
   {
    float delta = Time.deltaTime;

    inputManager.TickInput(delta);

    moveDirection = cameraObject.forward * inputManager.vertical;
    moveDirection += cameraObject.right * inputManager.horizontal;

    float speed = movementSpeed;
    moveDirection *= speed;

    Vector3 normalVector = Vector3.up;
    Vector3 projectVelocity = Vector3.ProjectOnPlane(moveDirection,normalVector);
    
    animationManager.UpdateAnimatorValues(inputManager.moveAmount,0);

    rigidbody.linearVelocity =projectVelocity;

    if (animationManager.canRotate)
    {
        HandleRotation(delta);
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
        targetDir.y =0;

        if(targetDir == Vector3.zero)
            targetDir = myTransform.forward;

        float rs = rotationSpeed;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation,tr,rs *delta);

        myTransform.rotation = targetRotation;
    }

    #endregion
}
