using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused;
    [SerializeField] GameObject pauseMenu;
    
    private PlayerControls playerControls;

    private void Awake()
    {
        // Initialize the input actions
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        // Enable the Pause action map
        playerControls.Pause.Enable();
        
        // Register the callback for the PauseMenu action
        playerControls.Pause.PauseMenu.performed += OnPausePerformed;
    }

    private void OnDisable()
    {
        // Clean up when the script is disabled
        playerControls.Pause.PauseMenu.performed -= OnPausePerformed;
        playerControls.Pause.Disable();
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        // Toggle pause state
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}