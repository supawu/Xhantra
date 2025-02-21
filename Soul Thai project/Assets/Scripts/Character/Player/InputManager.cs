using System;

using Unity.VisualScripting;
using UnityEngine;
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
  }
  public void MoveInput(float delta)
  {
    horizontal= movementInput.x;
    vertical=movementInput.y;
    moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
    mouseX = cameraInput.x;
    mouseY = cameraInput.y;
  }
}
