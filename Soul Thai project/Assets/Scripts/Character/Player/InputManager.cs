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
  public bool left;
  public bool right;
  public bool rollFlag;
  

  PlayerControls playercontrols;
  PlayerAttacker playerAttacker;
  PlayerInventory playerInventory;
  AudioManager audioManager;
 
  [SerializeField] Vector2 movementInput = Vector2.zero;
  [SerializeField] Vector2 cameraInput = Vector2.zero;

    void Awake()
    {//Get an acces as Component
        playerAttacker = GetComponent<PlayerAttacker>();
        playerInventory = GetComponent<PlayerInventory>();
        audioManager = GetComponent<AudioManager>();
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
    HandleAttackInput(delta);
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
 
  private void HandleAttackInput(float delta)
  {
    playercontrols.PlayerAction.Light.performed += i => left = true;
    playercontrols.PlayerAction.Heavy.performed += i => right = true;


    if(left)
    {
      playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
      audioManager.PlaySFX(audioManager.hit);
    }

    if(right)
    {
      playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
    }
    
  }
}
