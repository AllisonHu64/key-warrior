using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PianoTile : MonoBehaviour
{ 
    private Image bg;

    private GameObject myPanel;    
    private GamePanel myGame;

    private MyGrid parentGrid;
    private float spawnScaleTime = 1;
    private bool isPlayingSpawnAnim = false;
    private float clickedScaleTime = 1;
    private float clickedScaleTimeBack = 1;
    
    private bool isPlayingClickedAnim = false;

    private float movePosTime = 1;
    private bool isMoving = false;
    private Vector3 startMovePos, endMovePos;

    public Sprite[] bg_sprites = new Sprite[Const.ColumnNum];


    private void Awake() {
        bg = transform.GetComponent<Image>();
        myPanel = GameObject.Find("GamePanel");
        myGame = myPanel.GetComponent<GamePanel>();
    }
    // initialize
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
        startMovePos = transform.position;
        endMovePos = myGrid.transform.position;
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

    private void Update() {
        if (isPlayingSpawnAnim){
            // spawn animation slowest 1s
            if (spawnScaleTime <= 1){ // change this value if you want the slowest speed
                spawnScaleTime += Time.deltaTime * (1/myGame.speed); // 4(speed) is the speed value 1/4 seconds
                transform.localScale = Vector3.Lerp(Vector3.zero,Vector3.one, spawnScaleTime);
            }
            else{
                isPlayingSpawnAnim = false;
            }

        }

        if (isPlayingClickedAnim){
            // merge animation, go big
            if (clickedScaleTime <= 1 && clickedScaleTimeBack == 0){
                clickedScaleTime += Time.deltaTime * 1;
                transform.localScale = Vector3.Lerp(Vector3.one,Vector3.one*1.2f, clickedScaleTime);
            }

            // merge animation, go back to normal
            if (clickedScaleTime >= 1 && clickedScaleTimeBack <= 1){
                clickedScaleTimeBack += Time.deltaTime * 1;
                transform.localScale = Vector3.Lerp(Vector3.one*1.2f,Vector3.one, clickedScaleTimeBack);
            }
            
            if(clickedScaleTime >= 1 && clickedScaleTimeBack >= 1){
                isPlayingClickedAnim = false;
            }

        }

        if (isMoving){
            if (movePosTime <= 1){
                movePosTime += Time.deltaTime*4;
                transform.position = Vector3.Lerp(startMovePos,endMovePos, movePosTime);
            }
            else{
                isMoving = false;
            }
        }
        
    }
}
