using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldButton : MonoBehaviour
{
    public GameObject player;
    GameObject pauseMenu;
    public PauseMenu pausemenu;
    public GameObject setting;
   public void LoadWorld1(){
    if(Application.CanStreamedLevelBeLoaded(1)){//check if level can be load
        SceneManager.LoadScene(1);
    }
   }


    void Start()
    {
        setting.SetActive(false);
    }

    public void Resume()
    {
        pausemenu.Resume();
    }
    public void ResumeOption()
    {
        pausemenu.Resume();
        setting.SetActive(false);

    }
   

   public void LoadSettings()
    {
        setting.SetActive(true);
        
    }
    public void LoadSettingMenu()
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
        if (player != null)
        {
            Destroy(player);
        }
    }
}
