using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectLevelPanel : View
{
    // enter the level
    public void OnSelectLevelClick(string level){
        // select a level
        PlayerPrefs.SetString(Const.GameLevel, level);
        // Debug.Log(PlayerPrefs.GetString(Const.GameLevel));
        // go to the game scence
        SceneManager.LoadSceneAsync(1);
    }
}
