using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        // Hide and lock the cursor when the game starts
        HideCursor();
    }

    void Update()
    {
        // Example: Press Escape to show the cursor (for debugging or pausing)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowCursor();
        }

        // Example: Press Space to hide the cursor again
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HideCursor();
        }
    }

    public void HideCursor()
    {
        // Hide the cursor
        Cursor.visible = false;

        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowCursor()
    {
        // Show the cursor
        Cursor.visible = true;

        // Unlock the cursor
        Cursor.lockState = CursorLockMode.None;
    }
}