using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyGrid : MonoBehaviour
{
    public PianoTile pianoTile; // current number of this grid

    // check it has piano tile
    public bool IsHavePianoTile(){
        return pianoTile != null;
    }

    // get piano tile
    public PianoTile GetPianoTile(){
        return pianoTile;
    }


    // set the pianotile on this grid
    public void SetPianoTile(PianoTile pianoTile){
        this.pianoTile = pianoTile;

    }

    // se the grid color
    public void SetMyGridColor(Color color){
        //Fetch the Image component of the GameObject
        Image bg = GetComponent<Image>();
        bg.color = color;
    }
    
}
