using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeFlow : MonoBehaviour
{
    //pipe flow is used on each pipe to manage them while they are filling with waste
    //the script activates when another pipe uses adjacency to target the pipe
    //once targeted the pipe gets locked in position and fills based on the start point until it hits the end point
    public float FillAmount;
    public TileAdjacentList AdjacentChecker;
    public Transform Startpoint, EndPoint;
    public Transform Entry1, Entry2, Entry3, Entry4;
    public bool NoMove, IsLagoon;
    private PipeFlow NextFlow;
    private FlowEnd LastFlow;
    public float FillTimer;
    private GameObject FlowStarter;
    public int WasteAmount;
    private GameController gameController;
    private int Attempts;
    // Start is called before the first frame update
    public void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        if (FlowStarter == null)
        {
            FlowStarter = GameObject.Find("FlowStart");
        }
    }

    public void StartFlowing()
    {
        StartCoroutine(WasteFlowing());
    }
    public IEnumerator WasteFlowing()
    {
        NoMove = true;//prevents the player from touching the pipe
        if (CompareTag("Pipe"))//if the pipe is a regular pipe then it fills as normal
        {
            while (FillAmount < 100)
            {
                FillAmount += 10;
                yield return new WaitForSeconds(FillTimer);
            }
            CheckNewPipe();
        }
        else if (CompareTag("Lagoon"))//if its a lagoon then it will fill and decrease the waste counter
        {
            while (FillAmount < 100)
            {
                FillAmount += 10;
                //FlowStarter.GetComponent<WasteController>().CurWaste -= 10;
                FlowStarter.GetComponent<WasteController>().WasteDown();
                //Debug.Log(FlowStarter.GetComponent<WasteController>().CurWaste);
                //WasteAmount += 10;
                //Debug.Log(WasteAmount);
                yield return new WaitForSeconds(FillTimer);
            }
            CheckNewPipe();
        }
        else if (CompareTag("SLagoon"))//if its a small lagoon then it fills faster and reduces the waste counter
        {
            while (FillAmount < 100)
            {
                FillAmount += 25;
                //FlowStarter.GetComponent<WasteController>().CurWaste -= 10;
                FlowStarter.GetComponent<WasteController>().WasteDown();
                //Debug.Log(FlowStarter.GetComponent<WasteController>().CurWaste);
                //WasteAmount += 10;
                //Debug.Log(WasteAmount);
                yield return new WaitForSeconds(FillTimer);
            }
            CheckNewPipe();
        }

    }
    public void CheckNewPipe()//function for a pipe/lagoon to search and find if it can send waste to a nearby pipe, it will only send waste if both pipes allow travel in those directions and if the next pipe has no waste
    {
        //if the exit of the pipe is north
        //if (AdjacentChecker.South != null)
        //{
            if (EndPoint == AdjacentChecker.NTip)
            {
                //the nextflow object should equal the pipe directly above the previous one
            NextFlow = AdjacentChecker.North.GetComponentInChildren<PipeFlow>();
            if(AdjacentChecker.North == AdjacentChecker.NTip)
            {
                NextFlow = null;
            }
                //if the nextflow object is not empty and the entry 1 or 2 of the pipe is the same as the south transform
                //then the player does not lose
                if (NextFlow != null && NextFlow.NoMove == false)
                {
                    if (NextFlow.Entry1 == NextFlow.AdjacentChecker.STip)
                    {
                        //if entry equals the tip point then the start and endpoints are set and the flow begins
                        //if not the player will lose
                        NextFlow.Startpoint = NextFlow.Entry1;
                        NextFlow.EndPoint = NextFlow.Entry2;
                        NextFlow.StartFlowing();
                    }
                    if (NextFlow.Entry2 == NextFlow.AdjacentChecker.STip)
                    {
                        NextFlow.Startpoint = NextFlow.Entry2;
                        NextFlow.EndPoint = NextFlow.Entry1;
                        NextFlow.StartFlowing();
                    }
                    if (NextFlow.Entry3 != null)
                    {
                        if (NextFlow.Entry3 == NextFlow.AdjacentChecker.STip)
                        {
                            //if entry equals the tip point then the start and endpoints are set and the flow begins
                            //if not the player will lose
                            NextFlow.Startpoint = NextFlow.Entry3;
                            NextFlow.EndPoint = NextFlow.Entry1;
                            NextFlow.StartFlowing();
                        }
                    }
                    if (NextFlow.Entry4 != null)
                    {
                        if (NextFlow.Entry4 == NextFlow.AdjacentChecker.STip)
                        {
                            NextFlow.Startpoint = NextFlow.Entry4;
                            NextFlow.EndPoint = NextFlow.Entry2;
                            NextFlow.StartFlowing();
                        }
                    }
                    return;
                }
                if (IsLagoon && Attempts < 5)
                {
                    Debug.Log("RunNorth");
                    Attempts += 1;
                    EndPoint = Entry2;
                }
            }
        //}
        //if (AdjacentChecker.West != null)
        //{
            if (EndPoint == AdjacentChecker.ETip)
            {
                NextFlow = AdjacentChecker.East.GetComponentInChildren<PipeFlow>();
            if (AdjacentChecker.East == AdjacentChecker.ETip)
            {
                NextFlow = null;
            }
            LastFlow = AdjacentChecker.East.GetComponent<FlowEnd>();
                if (NextFlow != null && NextFlow.NoMove == false)
                {
                    if (NextFlow.Entry1 == NextFlow.AdjacentChecker.WTip)
                    {
                        NextFlow.Startpoint = NextFlow.Entry1;
                        NextFlow.EndPoint = NextFlow.Entry2;
                        NextFlow.StartFlowing();
                    }
                    if (NextFlow.Entry2 == NextFlow.AdjacentChecker.WTip)
                    {
                        NextFlow.Startpoint = NextFlow.Entry2;
                        NextFlow.EndPoint = NextFlow.Entry1;
                        NextFlow.StartFlowing();
                    }
                    if (NextFlow.Entry3 != null)
                    {
                        if (NextFlow.Entry3 == NextFlow.AdjacentChecker.WTip)
                        {
                            NextFlow.Startpoint = NextFlow.Entry3;
                            NextFlow.EndPoint = NextFlow.Entry1;
                            NextFlow.StartFlowing();
                        }
                    }
                    if (NextFlow.Entry4 != null)
                    {
                        if (NextFlow.Entry4 == NextFlow.AdjacentChecker.WTip)
                        {
                            NextFlow.Startpoint = NextFlow.Entry4;
                            NextFlow.EndPoint = NextFlow.Entry2;
                            NextFlow.StartFlowing();
                        }
                    }
                    return;
                }
                if (IsLagoon && Attempts < 5)
                {
                    Debug.Log("RunEast");
                    Attempts += 1;
                    EndPoint = Entry3;
                }
            }
        //}
        //if (AdjacentChecker.North != null)
        //{
            if (EndPoint == AdjacentChecker.STip)
            {
                NextFlow = AdjacentChecker.South.GetComponentInChildren<PipeFlow>();
            if (AdjacentChecker.South == AdjacentChecker.STip)
            {
                NextFlow = null;
            }
            if (NextFlow != null && NextFlow.NoMove == false)
                {
                    if (NextFlow.Entry1 == NextFlow.AdjacentChecker.NTip)
                    {
                        NextFlow.Startpoint = NextFlow.Entry1;
                        NextFlow.EndPoint = NextFlow.Entry2;
                        NextFlow.StartFlowing();
                    }
                    if (NextFlow.Entry2 == NextFlow.AdjacentChecker.NTip)
                    {
                        NextFlow.Startpoint = NextFlow.Entry2;
                        NextFlow.EndPoint = NextFlow.Entry1;
                        NextFlow.StartFlowing();
                    }
                    if (NextFlow.Entry3 != null)
                    {
                        if (NextFlow.Entry3 == NextFlow.AdjacentChecker.NTip)
                        {
                            NextFlow.Startpoint = NextFlow.Entry3;
                            NextFlow.EndPoint = NextFlow.Entry1;
                            NextFlow.StartFlowing();
                        }
                    }
                    if (NextFlow.Entry4 != null)
                    {
                        if (NextFlow.Entry4 == NextFlow.AdjacentChecker.NTip)
                        {
                            NextFlow.Startpoint = NextFlow.Entry4;
                            NextFlow.EndPoint = NextFlow.Entry2;
                            NextFlow.StartFlowing();
                        }
                    }
                    return;
                }
                if (IsLagoon && Attempts < 5)
                {
                    Debug.Log("RunSouth");
                    Attempts += 1;
                    EndPoint = Entry4;
                }
            }
        //}
        //if (AdjacentChecker.East != null)
        //{
            if (EndPoint == AdjacentChecker.WTip)
            {
                NextFlow = AdjacentChecker.West.GetComponentInChildren<PipeFlow>();
            if (AdjacentChecker.West == AdjacentChecker.WTip)
            {
                NextFlow = null;
            }
            if (NextFlow != null && NextFlow.NoMove == false)
                {
                    if (NextFlow.Entry1 == NextFlow.AdjacentChecker.ETip)
                    {
                        NextFlow.Startpoint = NextFlow.Entry1;
                        NextFlow.EndPoint = NextFlow.Entry2;
                        NextFlow.StartFlowing();
                    }
                    if (NextFlow.Entry2 == NextFlow.AdjacentChecker.ETip)
                    {
                        NextFlow.Startpoint = NextFlow.Entry2;
                        NextFlow.EndPoint = NextFlow.Entry1;
                        NextFlow.StartFlowing();
                    }
                    if (NextFlow.Entry3 != null)
                    {
                        if (NextFlow.Entry3 == NextFlow.AdjacentChecker.ETip)
                        {
                            NextFlow.Startpoint = NextFlow.Entry3;
                            NextFlow.EndPoint = NextFlow.Entry1;
                            NextFlow.StartFlowing();
                        }
                    }
                    if (NextFlow.Entry4 != null)
                    {
                        if (NextFlow.Entry4 == NextFlow.AdjacentChecker.ETip)
                        {
                            NextFlow.Startpoint = NextFlow.Entry4;
                            NextFlow.EndPoint = NextFlow.Entry2;
                            NextFlow.StartFlowing();
                        }
                    }
                    return;
                }
                if (IsLagoon && Attempts < 5)
                {
                    Debug.Log("RunWest");
                    Attempts += 1;
                    EndPoint = Entry1;
                CheckNewPipe();
            }
            }
            else
                gameController.Lost();
        //}
    }
}
