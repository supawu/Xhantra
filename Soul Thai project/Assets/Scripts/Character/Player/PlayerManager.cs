using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    public Animator anim;
    public bool isInteract;
    CameraHandler cameraHandler;
    Locomotion playerlocomotion;

    

     private void Awake()
    {
       cameraHandler = CameraHandler.singleton;
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
        

        inputManager.TickInput(delta);
        playerlocomotion.HandleMovement(delta);
        playerlocomotion.HandleRolling(delta);
    }

    private void FixedUpdate()
    {
       float delta = Time.fixedDeltaTime;

        if(cameraHandler != null)
        {
          cameraHandler.FollowTarget(delta);
          cameraHandler.HandleCameraRotation(delta, inputManager.mouseX, inputManager.mouseY);
        } 
    }
    

    private void LateUpdate()
    {
        inputManager.rollFlag = false;
    }


}
