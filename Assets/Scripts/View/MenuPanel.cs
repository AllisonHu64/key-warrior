using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class MenuPanel : MonoBehaviour
{
    public SelectLevelPanel selectLevelPanel;
    public SettingPanel settingPanel;

    // click on start game
    public void OnStartGameClick(){
        // change scene
        selectLevelPanel.Show();
    }

    // click on settings
    public void OnSettingsClick(){
        // show SettingPanel
        settingPanel.Show();
    }

    // click on exit game

    public void OnExitGameClick(){
        // exit game
        Application.Quit();
    }
}
