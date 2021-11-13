using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    //at the start of the game it will spawn pipes
    //when a pipe is removed a new one takes its place
    //when spawned the newest pipe type is placed on a 3 pipe cooldown before its seen again
    public GameObject[] Pipes;
    public int LastPipe = -1, LastPipe2 = -1, LastPipe3 = -1, LastPipe4 = -1, LastPipe5 = -1;
    public Transform[] PipeSpawns;
    public int ActivePipes, SpawnVal = -1, SpawnPointVal;
    public bool PipeLost;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPipes());
    }
    //coroutine that will spawn pipes whenever the spawn point value is lower then 3
    public IEnumerator SpawnPipes()
    {
        while(true)
        {
            if(ActivePipes < 3)
            {
                while(SpawnVal == LastPipe || SpawnVal == LastPipe2 || SpawnVal == LastPipe3 || SpawnVal == LastPipe4 || SpawnVal == LastPipe5)
                {
                    SpawnVal = Random.Range(0, 8);
                    yield return new WaitForSeconds(.01f);
                }
                //runs through the pipe data to prep a pipe to repeat after 3 cycles
                LastPipe5 = LastPipe4;
                LastPipe4 = LastPipe3;
                LastPipe3 = LastPipe2;
                LastPipe2 = LastPipe;
                LastPipe = SpawnVal;
                ActivePipes += 1;
                Instantiate(Pipes[SpawnVal], PipeSpawns[SpawnPointVal].position, PipeSpawns[SpawnPointVal].rotation, PipeSpawns[SpawnPointVal]);
                SpawnPointVal += 1;
            }
            yield return new WaitForSeconds(.5f);
        }
    }
    //when a pipe is removed this will activate making sure the right spawn point is chosen and a new pipe will take its place
    public IEnumerator LostPipe()//it checks each pipe spawn area and if there is a pipe child then it wont spawn a pipe
    {
        if(PipeSpawns[0].childCount < 1)
        {
            SpawnPointVal = 0;
        }
        if (PipeSpawns[1].childCount < 1)
        {
            SpawnPointVal = 1;
        }
        if (PipeSpawns[2].childCount < 1)
        {
            SpawnPointVal = 2;
        }
        ActivePipes -= 1;
        yield return new WaitForSeconds(1f);
        PipeLost = false;
    }
    //calls the coroutine above when used
    public void TakePipe()
    {
        if (PipeLost == false)
        {
            PipeLost = true;
            StartCoroutine(LostPipe());
        }
    }
}
