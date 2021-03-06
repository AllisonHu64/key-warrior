using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class GamePanel : MonoBehaviour
{
    public Text text_score; // current score

    public Text text_best_score; // best score

    public LosePanel losePanel;

    public WinPanel winPanel;

    public int score = 0;

    public int best_score = 0;

    public Transform girdParent; // parent of grid
    private int row; // number of rows

    private int col; // number of columns

    public GameObject tilePrefab;

    public GameObject pianoTilePrefab;

    public float timer = 1;

    public List<MyGrid> canCreatePianoTileGrid = new List<MyGrid>();

    public MyGrid[][] grids = null; // save all the generated grids

    public int rowsPlayed;

    private bool gameFinished = false;

    private int currentScore = 0;

    private int bestScore = 0;

    public AudioSource gameMusic;

    public AudioSource gameOverMusic;

    public AudioSource tileClearSound;

    //public AudioSource pressSource;
    //public AudioClip pressClip;

    private void Start()
    {
        gameMusic.volume = PlayerPrefs.GetFloat(Const.Music);
        tileClearSound.volume = PlayerPrefs.GetFloat(Const.Sound);
    }
    // restart
    public void OnRestartClick(){
        gameMusic.Stop();
        gameMusic.Play();
        rowsPlayed = 0;
        gameFinished = false;
        currentScore = 0;
        ClearAllPianoTiles();
        bestScore = PlayerPrefs.GetInt(Const.BestScore,0);
        gameMusic.Play();
    }

    public void ClearAllPianoTiles(){
        for(int i = 0; i < Const.RowNum; i ++){
            for (int j = 0; j < Const.ColumnNum; j++){
                if (this.grids[i][j].IsHavePianoTile()){
                    PianoTile pianoTile = this.grids[i][j].GetPianoTile();
                    // destory pianoTile
                    this.grids[i][j].SetPianoTile(null);
                    pianoTile.SetGrid(null);
                    GameObject.Destroy(pianoTile.gameObject);
                }
            }
        }
    }

    // exit
    public void OnExitClick(){
        Application.Quit();
    }

    public void onMenuClick(){
        SceneManager.LoadSceneAsync(0);
    }
    // initialize number of grids
    public void InitGrid(){

        // create number of grids
        // 4*6 cell size 45 * 45
        GridLayoutGroup gridLayoutGroup = girdParent.GetComponent<GridLayoutGroup>();
        gridLayoutGroup.constraintCount = 6;
        gridLayoutGroup.cellSize = new Vector2(45,45);

        // this is the current setting
        row = Const.RowNum; 
        col = Const.ColumnNum;
        this.grids = new MyGrid[row][];

        for (int i = 0; i < row; i++){
            for(int j = 0; j < col; j++){
                if(this.grids[i] == null){
                    this.grids[i] = new MyGrid[col];
                }
                // create i, j cell
                this.grids[i][j] = CreateGrid();
            }
        }
    }
    public MyGrid CreateGrid(){
        //create instances from profabs
        GameObject gameObject = GameObject.Instantiate(tilePrefab,girdParent);
        return gameObject.GetComponent<MyGrid>();
    }

    public void InitCanCreateTileGrid(){
        for (int j = 0; j < col; j++){
            canCreatePianoTileGrid.Add(grids[0][j]);
        }
    }

    public void CreatePianoTile(){
        // Randomly choose one of the elements 
        int tilesPerRow = RandomizerProbability();      // The int at the element is tile per row.
        IEnumerable<int> tileList = Enumerable.Range(1, col).OrderBy(x => Random.value).Take(tilesPerRow);
        // generate the a list of indexs
        foreach (int tileIndex in tileList)
        {
            PlacePianoTile(tileIndex);
        }

    } 
    public int RandomizerProbability(){
        /*Probability Ratios {zeroTile, oneTile, twoTiles, threeTiles}
        int[] easy = {2,1,7,0};
        int[] medium = {1,5,3,1};
        int[] hard = {0,3,1,6}; */

        string currentUserLevel = PlayerPrefs.GetString(Const.GameLevel, "easy");
        int multiplier = Const.GameLevelStartSpeedDict[currentUserLevel];

        if(rowsPlayed < (30 / multiplier)){                  //After 120 rowsPlayed
            PlayerPrefs.SetFloat(Const.GameSpeed, 1);
            Debug.Log("easy");
            return probabilityGenrtr(0);     //Release the tiles per row. 
        }
        else if(rowsPlayed < (60 / multiplier)){
            Debug.Log("medium"); 
            PlayerPrefs.SetFloat(Const.GameSpeed, 0.85f);
            return probabilityGenrtr(1);
        }
        else if(rowsPlayed < (100/ multiplier)){
            Debug.Log("hard");
            PlayerPrefs.SetFloat(Const.GameSpeed, 0.75f);
            return probabilityGenrtr(2);
        }
        else{
            Debug.Log("hell");
            PlayerPrefs.SetFloat(Const.GameSpeed, 0.5f);
            return probabilityGenrtr(3);
        }
    }

    // to do should add constants to const file
    public int probabilityGenrtr(int level){
        int tilesPerRowIndex = Random.Range(0,10);
        if(level == 0){  
            return tilesPerRowIndex >= 3 ? 1 : 0;
        }
        else if(level == 1){
            return (tilesPerRowIndex >= 3 ? 1 : 0) + (tilesPerRowIndex >= 8 ? 1 : 0);
        }
        else if(level == 2){
            return (tilesPerRowIndex >= 3 ? 1 : 0) + (tilesPerRowIndex >= 7 ? 1 : 0) + (tilesPerRowIndex >= 8 ? 1 : 0);
        }
        else{
            return 1 + (tilesPerRowIndex >= 2 ? 1 : 0) + (tilesPerRowIndex >= 6 ? 1 : 0);
        }
    // Return the tiles per row.
    }

    public void PlacePianoTile(int index){
        GameObject gameObject = GameObject.Instantiate(pianoTilePrefab,canCreatePianoTileGrid[index].transform);
        gameObject.GetComponent<PianoTile>().Init(canCreatePianoTileGrid[index], (PlayerKeyType)index);
    }

    private void Awake() {
        // initate grid
        InitGrid();
        InitCanCreateTileGrid();
        CreatePianoTile();
        rowsPlayed = 0;
        bestScore = PlayerPrefs.GetInt(Const.BestScore,0);
        text_best_score.text = PlayerPrefs.GetInt(Const.BestScore, 0).ToString();
    }

    // move all the piano tiles go down by one
    public void MoveDown(){
        rowsPlayed++;
        for(int i = row-1; i >=0; i --){
            for(int j = col-1; j >= 0; j--){
                this.grids[i][j].SetMyGridColor(new Color(0,0,0,0));
                if (this.grids[i][j].IsHavePianoTile()){
                    // check if PianoTile is at the bottom
                    PianoTile pianoTile = this.grids[i][j].GetPianoTile();
                    if (i == row-1){
                        // you lose
                        Debug.Log("You Lose!");
                        gameMusic.Stop();
                        gameOverMusic.Play();
                        this.gameFinished = true;
                        if (bestScore < currentScore){
                            PlayerPrefs.SetInt(Const.BestScore,currentScore);
                            winPanel.Show();
                        }
                        else{
                            losePanel.Show();
                        }
                    }
                    else{
                        if (i == row - 2){
                            this.grids[i+1][j].SetMyGridColor(Color.white);
                        }
                        pianoTile.MoveToGrid(grids[i+1][j]);      
                    }

                }
            }
        }
        CreatePianoTile();
    }

    // clearing tiles
    public void OnClickTile(int i){
        if (this.grids[Const.RowNum-1][i].IsHavePianoTile()){
            //pressSource.PlayOneShot(pressClip);
            currentScore++;
            tileClearSound.Play();
            PianoTile pianoTile = this.grids[Const.RowNum-1][i].GetPianoTile();
            this.grids[Const.RowNum-1][i].SetPianoTile(null);
            pianoTile.SetGrid(null);
            GameObject.Destroy(pianoTile.gameObject);
            Debug.Log("destoryed Tiletype : " + (PlayerKeyType) i);

        }
    }

    void Update()
    {
        float speed = PlayerPrefs.GetFloat(Const.GameSpeed, 1);

        // rolling grid
        if ( (!gameFinished) && (timer > speed)) // this is in seconds
        {
            timer = 0;
            MoveDown();
        }
        timer += UnityEngine.Time.deltaTime;

        // clearing keys
        for(int i = 0; (i < Const.ColumnNum) && (!gameFinished); i ++){
            if (Input.GetKeyUp(Const.KeyPressList[i]))
            {
                // clear keys
                OnClickTile(i);
            }
        }

        // set best score
        text_score.text = currentScore.ToString();
        bestScore = PlayerPrefs.GetInt(Const.BestScore,0);
        if (bestScore < currentScore){
            text_best_score.text = currentScore.ToString();
        }
        else{
            text_best_score.text = bestScore.ToString();
        }
    }

}
