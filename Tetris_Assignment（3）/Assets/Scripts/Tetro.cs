using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetro : MonoBehaviour
{
    float fall = 0;
    public float fallspeed = 1;
    public bool allowrotation = true;
    public bool limitrotation = false;
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
                FindObjectOfType<Game>().updategrid(this);
            }
            else
            {
                transform.position += Vector3.left;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (InsideFrame())
            {
                FindObjectOfType<Game>().updategrid(this);
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
                FindObjectOfType<Game>().updategrid(this);
            }
            else
            {
                transform.position += Vector3.up;
                enabled = false;
                FindObjectOfType<Game>().SpawnNext();//when one set down another one keep falling


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


                if (InsideFrame())
                {
                    FindObjectOfType<Game>().updategrid(this);
                }
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


        bool InsideFrame()
        {
            foreach (Transform Block in transform)
            {
                Vector2 pos = FindObjectOfType<Game>().Round(Block.position);
                if (FindObjectOfType<Game>().InsideFrame(pos) == false)
                {
                    return false;
                }
                if (FindObjectOfType<Game>().GetTransformAtGridPosition(pos) != null && FindObjectOfType<Game>().GetTransformAtGridPosition(pos).parent != transform)
                {
                    return false;

                }
            }
            return true; //back to checkinput

        }
    }
}

