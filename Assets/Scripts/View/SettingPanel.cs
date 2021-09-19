using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : View
{
    public Slider slider_sound;
    public Slider slider_music;

    // move music scroll bar
    public void OnMusicVolumeChange(float volume){
        PlayerPrefs.SetFloat(Const.Music, volume);
        Debug.Log(PlayerPrefs.GetFloat(Const.Music));
    }

    // move sound scroll bar
    public void OnSoundVolumeChange(float volume){
        PlayerPrefs.SetFloat(Const.Sound, volume);
        Debug.Log(PlayerPrefs.GetFloat(Const.Sound));
    }

    // initialize music and sound slider when restarting the game

    public override void Show()
    {
        base.Show();
        slider_music.value = PlayerPrefs.GetFloat(Const.Music,0);
        slider_sound.value = PlayerPrefs.GetFloat(Const.Sound,0);

    }
}
