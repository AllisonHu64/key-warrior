using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PianoTile : MonoBehaviour
{ 
    // background image
    private Image bg;

    // the grid that the piano tile is on
    private MyGrid parentGrid;

    // the duration of spawn animation
    private float spawnScaleTime = 1;

    // control (TODO: use unity animation)
    private bool isPlayingSpawnAnim = false;

    // the duration of clearing tile animation (not implemented)
    private float clickedScaleTime = 1;
    private float clickedScaleTimeBack = 1;

    // control for runing clearing animation    
    private bool isPlayingClickedAnim = false;

    // duration of moving animation
    private float movePosTime = 1;

    // control for moving tile animation
    private bool isMoving = false;

    // startMovePos and endMovePos (for moving tiles)
    private Vector3 startMovePos, endMovePos;

    // list of available background sprites
    public Sprite[] bg_sprites = new Sprite[Const.ColumnNum];


    // life cycle function
    private void Awake() {
        bg = transform.GetComponent<Image>();
    }

    // initialize the piano tile
    public void Init(MyGrid myGrid, PlayerKeyType pkt){
        myGrid.SetPianoTile(this);
        this.SetGrid(myGrid);
        this.bg.sprite = this.bg_sprites[(int)pkt];
        
        PlaySpwanAnim();
    }

    public void SetGrid(MyGrid myGrid){
        parentGrid = myGrid;
    }

    public MyGrid GetGrid(){
        return parentGrid;
    }

    public void MoveToGrid(MyGrid myGrid){
        transform.SetParent(myGrid.transform);
        startMovePos = transform.localPosition;
        isMoving = true;
        movePosTime = 0;
        
        GetGrid().SetPianoTile(null);
        myGrid.SetPianoTile(this);
        SetGrid(myGrid);
    }

    // play spawn 
    public void PlaySpwanAnim(){
        // triggers Update
        spawnScaleTime = 0;
        isPlayingSpawnAnim = true;

    }

    public void PlayMergeAnim(){
        clickedScaleTime = 0;
        clickedScaleTimeBack = 0;
        isPlayingClickedAnim = true;
    }

    public void PlayMoveAnim(){
        isMoving = true;
        movePosTime = 0;
    }

    // life cycle function 
    private void Update() {
        float speed = PlayerPrefs.GetFloat(Const.GameSpeed, 1);

        if (isPlayingSpawnAnim){
            // spawn animation slowest 1s
            if (spawnScaleTime <= 1){ // change this value if you want the slowest speed

                spawnScaleTime += Time.deltaTime * (4/speed);
                // 2 means the animation ends in 1/2 seconds
                // 4 means the animation ends in 1/4 sencinds
                // 1/4 means the animation ends in 4 seconds
                transform.localScale = Vector3.Lerp(Vector3.zero,Vector3.one, spawnScaleTime);
            }
            else{
                isPlayingSpawnAnim = false;
            }

        }

        if (isPlayingClickedAnim){
            // merge animation, go big
            if (clickedScaleTime <= 1 && clickedScaleTimeBack == 0){
                clickedScaleTime += Time.deltaTime * 1/speed;
                transform.localScale = Vector3.Lerp(Vector3.one,Vector3.one*1.2f, clickedScaleTime);
            }

            // merge animation, go back to normal
            if (clickedScaleTime >= 1 && clickedScaleTimeBack <= 1){
                clickedScaleTimeBack += Time.deltaTime * 1/speed;
                transform.localScale = Vector3.Lerp(Vector3.one*1.2f,Vector3.one, clickedScaleTimeBack);
            }
            
            if(clickedScaleTime >= 1 && clickedScaleTimeBack >= 1){
                isPlayingClickedAnim = false;
            }

        }

        if (isMoving){
            if (movePosTime <= 1){
                movePosTime += Time.deltaTime*4/speed;
                transform.localPosition = Vector3.Lerp(startMovePos,Vector3.zero, movePosTime);
            }
            else{
                isMoving = false;
            }
        }
        
    }
}
