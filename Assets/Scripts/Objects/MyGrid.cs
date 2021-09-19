using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyGrid : MonoBehaviour
{
    public PianoTile pianoTile; // current number of this grid

    // check if have number
    public bool IsHavePianoTile(){
        return pianoTile != null;
    }


    public PianoTile GetPianoTile(){
        return pianoTile;
    }


    public void SetPianoTile(PianoTile pianoTile){
        this.pianoTile = pianoTile;

    }

    public void SetMyGridColor(Color color){
        //Fetch the Image component of the GameObject
        Image bg = GetComponent<Image>();
        bg.color = color;
    }
    
}
