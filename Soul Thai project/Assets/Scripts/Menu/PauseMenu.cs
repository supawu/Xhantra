using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused;
    [SerializeField] GameObject pauseMenu;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
       Resume();
    }

    private void OnEnable()
    {
        // Enable the input actions
        playerControls.Enable();

        // Subscribe to the pause action
        playerControls.Pause.PauseMenu.performed += OnPausePerformed;
        Debug.Log("Input system enabled. Listening for pause input.");
    }
    

 

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Pause key pressed.");

        // Toggle pause when the pause action is performed
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
        Time.timeScale = 0f; // Freeze time
        isPaused = true;
        Debug.Log("Game Paused. Time Scale: " + Time.timeScale);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Unfreeze time
        isPaused = false;
        Debug.Log("Game Resumed. Time Scale: " + Time.timeScale);
    }
}