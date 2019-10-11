using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static int framewidth = 9;
    public static int frameheight = 18;
    public static Transform[,] grid = new Transform[framewidth, frameheight];
    // Start is called before the first frame update

    public void updategrid(Tetro tetromino)
    {
        for (int y = 0; y < frameheight; ++y)
        {
            for (int x = 0; x < framewidth; ++x)
            {
                if (grid[x, y] != null) {
                    if (grid[x, y].parent == tetromino.transform)
                    {
                        grid[x, y] = null;
                    }

                }
            }

        }

        foreach (Transform mino in tetromino.transform) {
            Vector2 pos = Round (mino.position);
            if (pos.y < frameheight){
                grid[(int)pos.x, (int)pos.y] = mino;
            
        }
    }

    void Start()
    {
        SpawnNext();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnNext()
    {
        GameObject NextOne = (GameObject)Instantiate(Resources.Load(getRandomOne(),typeof(GameObject)), new Vector2(0.5f, 20.0f), Quaternion.identity);
    }
    public bool InsideFrame(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < framewidth && (int)pos.y >= 0);
    }
    public Vector2 Round(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));

    }
    
    string getRandomOne() {
        int randomOne = Random.Range(1, 8);
        string randomOneName = "Prefabs/(i)";
        switch (randomOne) {
            case 1:
                randomOneName = "Prefabs/(t)";
                break;
            case 2:
                randomOneName = "Prefabs/(o)";
                break;
            case 3:
                randomOneName = "Prefabs/(s)";
                break;
            case 4:
                randomOneName = "Prefabs/(z)";
                break;
            case 5:
                randomOneName = "Prefabs/(l)";
                break;
            case 6:
                randomOneName = "Prefabs/(i)";
                break;
            case 7:
                randomOneName = "Prefabs/(j)";
                break;
                
        }
        return randomOneName;

    }
}
