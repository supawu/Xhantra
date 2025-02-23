using System;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
  public static InputManager instance;
  public float horizontal;
  public float vertical;
  public float moveAmount;
  public float mouseX;
  public float mouseY;
  public bool b_input;
  public bool rollFlag;
  public bool isInteract;

  PlayerControls playercontrols;
  CameraHandler cameraHandler;
  [SerializeField] Vector2 movementInput = Vector2.zero;
  [SerializeField] Vector2 cameraInput = Vector2.zero;

    private void Awake()
    {
       cameraHandler = CameraHandler.singleton;
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;

        if(cameraHandler != null)
        {
          cameraHandler.FollowTarget(delta);
          cameraHandler.HandleCameraRotation(delta, mouseX,mouseY);
        }
    }

    private void OnEnable()//Getting input method 
  {
    if (playercontrols == null) //Check if there is control
    {
      playercontrols = new PlayerControls(); //Create control object

      playercontrols.PlayerMovement.Movements.performed += i => movementInput = i.ReadValue<Vector2>();//Configure input
      playercontrols.PlayerMovement.Movements.canceled += i => movementInput = Vector2.zero;  // Reset when no input
      playercontrols.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();//Configure input
      playercontrols.PlayerMovement.Camera.canceled += i => cameraInput = Vector2.zero;  // Reset when no input

    }
    playercontrols.Enable();
  }

  private void OnDisable()
  {
    playercontrols.Disable();
  }
  public void TickInput(float delta)
  {
    MoveInput(delta);
    HandleRollInput(delta);
  }
  public void MoveInput(float delta)
  {
    horizontal= movementInput.x;
    vertical=movementInput.y;
    moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
    mouseX = cameraInput.x;
    mouseY = cameraInput.y;
  }

  private void HandleRollInput(float delta)
  {
    b_input = playercontrols.PlayerAction.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Performed;

    if(b_input)
    {
      rollFlag = true;
     
    }
   
  }
 
}
