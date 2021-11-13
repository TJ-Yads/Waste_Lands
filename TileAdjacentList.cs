using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAdjacentList : MonoBehaviour
{
    //adjacent list will check and track what game objects are next to itself
    // each pipe and tile uses adjacent lists to manage waste and placements
    public Transform North, East, South, West;
    public Transform NTip, ETip, STip, WTip;
    Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(CheckAdjacents());
    }
    public IEnumerator CheckAdjacents()//this will check the gameobject and then find what objects are in each direction of it while adding it to the list
    {
        yield return new WaitForSeconds(1f);
        RaycastHit2D hit2 = Physics2D.Raycast(NTip.position, Vector2.up);//fire a ray north or upward
        if (hit2.collider != null)//if it hits something then the transform it hit becomes the north adjacent object
        {
            North = hit2.transform;
        }
        else//if it hits nothing then it sets the adjacent object as itself
        {
            North = NTip;
        }
        RaycastHit2D hit4 = Physics2D.Raycast(ETip.position, Vector2.right);
        if (hit4.collider != null)
        {
            East = hit4.transform;
        }
        else
        {
            East = ETip;
        }
        RaycastHit2D hit = Physics2D.Raycast(STip.position, -Vector2.up);
        if (hit.collider != null)
        {
            South = hit.transform;
        }
        else
        {
            South = STip;
        }
        RaycastHit2D hit3 = Physics2D.Raycast(WTip.position, -Vector2.right);
        if (hit3.collider != null)
        {
            West = hit3.transform;
        }
        else
        {
            West = WTip;
        }
    }
    public void AdjacentRun()
    {
        StartCoroutine(CheckAdjacents());
    }
}
