using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Game : MonoBehaviour
{
    public static int w = 9;
    public static int h = 18;
    public static Transform[,] grid = new Transform[w, h];
    public int score1Line = 20;
    public int score2Line = 50;
    public int score3Line = 180;
    public int score4Line = 500;
    public Text hud_score;
    private int numberofrows = 0;
    private int currentScore = 0;
    private AudioSource audioSource;
    public AudioClip good;
    public AudioClip amazing;
    public AudioClip excellent;
    public AudioClip unbelieveable;


    // Start is called before the first frame update

    public static Vector2 roundVec2(Vector2 v) {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }
    void Start() {
        SpawnerNext();
        audioSource = GetComponent<AudioSource>();
    }
    

    void Update() {
        UpdateScore();
        UpdateUI();
    }
    void UpdateUI() {
        hud_score.text = currentScore.ToString();
    }
    void UpdateScore() {

        if (numberofrows > 0) {
            if (numberofrows == 1){
                Clear1Line();
                Sgood();

            }
            else if (numberofrows == 2) {
                Clear2Line();
                Samazing();
            }
            else if (numberofrows == 3) {
                Clear3Line();
                Sexcellent();
                
            }
            else if (numberofrows == 4) {
                Clear4Line();
                Sunbelieveable();
                
            }
        }
        numberofrows = 0;
        
    }
    public void Sgood()
    {
        audioSource.PlayOneShot(good);
    }

    public void Samazing()
    {
        audioSource.PlayOneShot(amazing);
    }
    public void Sexcellent()
    {
        audioSource.PlayOneShot(excellent);
    }
    public void Sunbelieveable()
    {
        audioSource.PlayOneShot(unbelieveable);
    }
    public void Clear1Line() {
        currentScore += score1Line;
        
        
    }
    public void Clear2Line() {
        currentScore += score2Line;
    }
    public void Clear3Line() {
        currentScore += score3Line;
    }
    public void Clear4Line() {
        currentScore += score4Line;
    }


    public static bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 &&
        (int)pos.x < w &&
        (int)pos.y >= 0);
    }
    public void SpawnerNext() {
        GameObject nextTertromino = (GameObject)Instantiate(Resources.Load(GetRandomTetromino(), typeof(GameObject)), new Vector2(5.0f, 20.0f), Quaternion.identity);
    }
    string GetRandomTetromino() {
        int randomT = Random.Range(1, 8);
        string randomTetrominoName = "Prefabs/(i)";
        switch (randomT) {
            case 1:
                randomTetrominoName = "Prefabs/(s)";
                break;
            case 2:
                randomTetrominoName = "Prefabs/(z)";
                break;
            case 3:
                randomTetrominoName = "Prefabs/(j)";
                break;
            case 4:
                randomTetrominoName = "Prefabs/(j)";
                break;
            case 5:
                randomTetrominoName = "Prefabs/(t)";
                break;
            case 6:
                randomTetrominoName = "Prefabs/(o)";
                break;
            case 7:
                randomTetrominoName = "Prefabs/(i)";
                break;
            case 8:
                randomTetrominoName = "Prefabs/(s)";
                break;
        }
        return randomTetrominoName;
    }
    public void deleteRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }
    public void decreaseRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != null)
            {
                // Move one towards bottom
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                // Update Block position
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }
    public void decreaseRowsAbove(int y)
    {
        for (int i = y; i < h; ++i)
            decreaseRow(i);
    }
    public bool isRowFull(int y)
    {
        for (int x = 0; x < w; ++x) { 
            if (grid[x, y] == null)
            {
                return false;
            }
    }
        numberofrows++;
        return true;
    }
    public void deleteFullRows()
    {
        for (int y = 0; y < h; ++y)
        {
            if (isRowFull(y))
            {
                deleteRow(y);
                decreaseRowsAbove(y + 1);
                --y;
                
            }
        }
    }
    
    }





    