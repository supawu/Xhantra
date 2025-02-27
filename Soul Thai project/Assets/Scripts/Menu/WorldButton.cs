using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldButton : MonoBehaviour
{

    GameObject pauseMenu;
   public void LoadWorld1(){
    if(Application.CanStreamedLevelBeLoaded(1)){//check if level can be load
        SceneManager.LoadScene(1);
    }
   }


    public void Pause()
    {
        pauseMenu.SetActive(true);
    }
    
    public void Resume()
    {
        pauseMenu.SetActive(false);
    }

   public void LoadSettings()
    {
        if (Application.CanStreamedLevelBeLoaded("Settings"))
        {
            SceneManager.LoadScene("Settings");
        }
    }

     public void ExitGame()
    {
        Debug.Log("Game is exiting...");
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        if (Application.CanStreamedLevelBeLoaded("Scene_Menu"))
        {
            SceneManager.LoadScene("Scene_Menu");
        }
    }
}
