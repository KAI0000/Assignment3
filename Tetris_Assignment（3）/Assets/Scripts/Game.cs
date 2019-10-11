using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static int w = 9;
    public static int h = 18;
    public static Transform[,] grid = new Transform[w, h];
    // Start is called before the first frame update
    
    public static Vector2 roundVec2(Vector2 v) {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }
    void Start() {
        SpawnerNext();
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
    public static void deleteRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }
    public static void decreaseRow(int y)
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
    public static void decreaseRowsAbove(int y)
    {
        for (int i = y; i < h; ++i)
            decreaseRow(i);
    }
    public static bool isRowFull(int y)
    {
        for (int x = 0; x < w; ++x)
            if (grid[x, y] == null)
                return false;
        return true;
    }
    public static void deleteFullRows()
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





    