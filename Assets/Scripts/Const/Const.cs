using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const
{
    public const string GameLevel = "game_level";
    public const string Sound = "sound";
    public const string Music = "music";

    public static Dictionary<string,int> GameLevelStartSpeedDict = new Dictionary<string,int>(){
        {"easy", 1},
        {"medium", 2},
        {"hard", 3}
    };

    public int CurrentSpeed = 1;

    public const string BestScore = "best_score";
    
    public const int RowNum = 4;

    public const int ColumnNum = 6;

    public static KeyCode[] KeyPressList = new KeyCode[6]{
        KeyCode.Q, KeyCode.A, KeyCode.W, KeyCode.S, KeyCode.E, KeyCode.D
    };

}
