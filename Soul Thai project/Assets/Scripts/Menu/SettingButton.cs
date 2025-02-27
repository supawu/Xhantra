using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingButton : MonoBehaviour
{
   public void LoadWorld1(){
    if(Application.CanStreamedLevelBeLoaded(1)){//check if level can be load
        SceneManager.LoadScene("Settings");
    }
   }
}
