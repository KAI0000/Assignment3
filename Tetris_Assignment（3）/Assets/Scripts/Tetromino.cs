using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    float fall = 0;
    public float fallspeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        
    }
    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right;//or new Vector3(1, 0, 0); attampt move
            if (InsideFrame())
            {
            }
            else {
            transform.position += Vector3.left;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (InsideFrame())
            {
            }
            else
            {
                transform.position += Vector3.right;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Mathf.Round(Time.time) - fall == fallspeed)
        {
            transform.position += Vector3.down;
            if (InsideFrame())
            {
            }
            else
            {
                transform.position += Vector3.up;
            }
            fall = Mathf.Round(Time.time);
        }


        //else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - fall >= fallspeed)
        /*{
            transform.position += new Vector3(0, -1, 0);
            fall = Time.time;
            }*/
        
    else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate (0, 0, 90);
            if (InsideFrame())
            {
            }
            else
            {
                transform.Rotate(0, 0, 90);
            }
        }
    }

    bool InsideFrame ()
    {
        foreach (Transform mino in transform)
        {
            Vector2 pos = FindObjectOfType<Game>().Round(mino.position);
            if (FindObjectOfType<Game>().InsideFrame(pos) == false)
            {
                return false;
            }
        }
                return true; //back to checkinput
                
        

    }

}
