using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetro : MonoBehaviour
{
    float lastFall = 0;
    public bool allowrotation = true;
    public bool limitrotation = false;
    public AudioClip moveSound;
    public AudioClip rotation;
    
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Game.roundVec2(child.position);

            // Not inside Border?
            if (!Game.insideBorder(v))
                return false;

            // Block in grid cell (and not part of same group)?
            if (Game.grid[(int)v.x, (int)v.y] != null &&
                Game.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }
    void updateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < Game.h; ++y)
            for (int x = 0; x < Game.w; ++x)
                if (Game.grid[x, y] != null)
                    if (Game.grid[x, y].parent == transform)
                        Game.grid[x, y] = null;

        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 v = Game.roundVec2(child.position);
            Game.grid[(int)v.x, (int)v.y] = child;
        }
    }
    void Update()
    {
        // Move Left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Modify position
            transform.position += new Vector3(-1, 0, 0);

            // See if valid
            if (isValidGridPos())
            // Its valid. Update grid.
            { updateGrid(); MoveAudio(); }

            else
            // Its not valid. revert.
            transform.position += new Vector3(1, 0, 0); 
        }

        // Move Right
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Modify position
            transform.position += new Vector3(1, 0, 0);

            // See if valid
            if (isValidGridPos())
            // It's valid. Update grid.
            { updateGrid(); MoveAudio(); }
            else
                // It's not valid. revert.
                transform.position += new Vector3(-1, 0, 0);
        }
        // Rotate
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (allowrotation)
            {
                if (limitrotation)
                {
                    if (transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }


                // See if valid
                if (isValidGridPos())
                // It's valid. Update grid.
                { updateGrid(); RotationAudio(); }
                else
                {
                    if (limitrotation)
                    {
                        if (transform.rotation.eulerAngles.z >= 90)
                        {
                            transform.Rotate(0, 0, -90);
                        }

                        else
                        {
                            transform.Rotate(0, 0, 90);
                        }
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
            }
        }

        // Fall
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Modify position
            transform.position += new Vector3(0, -1, 0);

            // See if valid
            if (isValidGridPos())
            {
                // It's valid. Update grid.
                updateGrid(); MoveAudio();
            }
            else
            {
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                // Clear filled horizontal lines
                FindObjectOfType<Game>().deleteFullRows(); 

                // Spawn next Group
                FindObjectOfType<Game>().SpawnerNext();

                // Disable script
                enabled = false;
            }
        }
        // Move Downwards and Fall
        else if (Input.GetKeyDown(KeyCode.DownArrow) ||
                 Time.time - lastFall >= 1)
        {
            // Modify position
            transform.position += new Vector3(0, -1, 0);

            // See if valid
            if (isValidGridPos())
            {
                // It's valid. Update grid.
                updateGrid(); 
            }
            else
            {
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                // Clear filled horizontal lines

                FindObjectOfType<Game>().deleteFullRows();
                
                
                // Spawn next Group
                FindObjectOfType<Game>().SpawnerNext();

                // Disable script
                enabled = false;
            }

            lastFall = Time.time;
        }

    }
    void MoveAudio() {
        audioSource.PlayOneShot(moveSound);
    }
    void RotationAudio() {
        audioSource.PlayOneShot(rotation);
    }
   


}

