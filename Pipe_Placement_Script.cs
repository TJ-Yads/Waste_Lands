using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe_Placement_Script : MonoBehaviour
{
    //allows the player to pickup and place pipes on the game map
    private Vector3 mousePosition;
    bool mouseOver = false;
    bool overLap = false;

    //Self is used for positioning when placed on tiles
    public GameObject Self;
    //is held is used to check if the object is in the players hand
    private bool IsHeld;
    public TileAdjacentList AdjacentChecker;
    public PipeFlow pipeflow;
    private bool WasHeld = false;

    void FixedUpdate()
    {
        //find mouse and track its postion in game
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        if (mouseOver)
        {
            if (Input.GetMouseButton(0) == false && pipeflow.NoMove == false && WasHeld == false)//if the player clicks on a pipe that can be moved the it attachs to the mouse for placement
            {
                if (!overLap)
                {
                    transform.SetParent(null);
                    IsHeld = true;
                    transform.position = Vector2.Lerp(transform.position, mousePosition, 100f);
                }
            }
            else
            {
                IsHeld = false;
                WasHeld = true;
            }
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0) == true)
        {
            mouseOver = true;
        }
    }

    void OnMouseExit()
    {
        if (Input.GetMouseButton(0) == true || WasHeld == true)
        {
            mouseOver = false;
            WasHeld = false;
        }
    }

    //used to connect a pipe to a tile
    private void OnTriggerStay2D(Collider2D collision)//while hovering over a tile area if the pipe can be placed then the pipe will attach when clicked
    {
        if (collision.tag == "Tile" && IsHeld == false)
        {
            Self.transform.SetParent(collision.transform);
            Self.transform.localPosition = Vector3.zero;
            AdjacentChecker.AdjacentRun();
        }
    }
    //used to respawn a pipe once picked up
    private void OnTriggerExit2D(Collider2D collision)//when the player removes a pipe from the spawn area it will update and make a new pipe
    {
        if(collision.tag == "Spawner")
        {
            GameObject Spawn = collision.gameObject;
            PipeSpawner React = Spawn.GetComponentInParent<PipeSpawner>();
            React.TakePipe();
        }
    }
}
