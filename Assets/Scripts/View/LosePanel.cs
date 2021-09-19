using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 

public class LosePanel : View
{
    public void OnExitClick(){
        Application.Quit();
    }

    public void onMenuClick(){
        SceneManager.LoadSceneAsync(0);
    }
}
