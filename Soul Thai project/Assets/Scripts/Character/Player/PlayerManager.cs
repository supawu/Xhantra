using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    public Animator anim;
    public bool isInteract;
    CameraHandler cameraHandler;
    Locomotion playerlocomotion;

    [Header("Player Flags")]
    public bool isUsingRightHand;
    public bool isUsingLeftHand;

    private void Awake()
    {
        cameraHandler = FindFirstObjectByType<CameraHandler>();
    }
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        anim = GetComponentInChildren<Animator>();
        playerlocomotion = GetComponent<Locomotion>();
    }

    void Update()
    {
        float delta = Time.deltaTime;

        isInteract = anim.GetBool("isInteract");
        isUsingRightHand = anim.GetBool("isUsingRightHand");
        isUsingLeftHand = anim.GetBool("isUsingLeftHand");

        inputManager.TickInput(delta);
        playerlocomotion.HandleMovement(delta);
       
        playerlocomotion.HandleRolling(delta);
        
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;

        if (cameraHandler != null)
        {
            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRotation(delta, inputManager.mouseX, inputManager.mouseY);
        }
    }


    private void LateUpdate()//when hit the button at the end of the frame it stops
    {
        inputManager.rollFlag = false;
        inputManager.left = false;
        inputManager.right = false;

        if (!anim.GetBool("isInteract"))
    {
        isInteract = false;
    }
    }


}
