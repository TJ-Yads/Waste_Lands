using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawnSystem : MonoBehaviour
{
    //at the start of each game the tile spawner will create the player map
    //the map uses tiles and will randomly place a tile at each point
    //the special tiles such as houses or lagoons have a lower chance to spawn and have a spawn limit
    public GameObject[] Tiles;
    public int HorizontalMax, VerticalMax, TileMax, MaxHouse, MaxWater, MaxLLagoon, MaxSLagoon;
    public Transform SpawnPoint, LastSpawn;
    private int CurHor, HorizontalPos, VerticalPos, CurWater, CurHouse, CurLLagoon, CurSLagoon, SpawnVal, SpecialLimit;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTiles());
    }
    IEnumerator SpawnTiles()
    {
        //set limits on the max amount of special tiles
        CurHouse = MaxHouse;
        CurWater = MaxWater;
        CurLLagoon = MaxLLagoon;
        CurSLagoon = MaxSLagoon;

        for (int i = 0; i < TileMax; i++)
        {
            SpawnVal = Random.Range(0, 17);
            //if any of these conditions are met then spawn value will become zero leading to the tile becoming a grass tile
            //activates if house, water, lagoon limit is 0 or if the spawn value is above 4 which is used to increase chance of grass
            if (SpawnVal == 3 && CurHouse <= 0 || SpawnVal == 4 && CurWater <= 0 || SpawnVal == 1 && CurLLagoon <= 0 || SpawnVal == 2 && CurSLagoon <= 0 || SpawnVal >= 5 || SpecialLimit >=3)
            {
                //sets tile as grass
                SpawnVal = 0;
            }
            else if (SpawnVal == 1)
            {
                //sets tile as a large lagoon
                CurLLagoon -= 1;
                MaxLLagoon -= 1;
                SpecialLimit += 1;
            }
            else if (SpawnVal == 2)
            {
                //sets tile as a small lagoon
                CurSLagoon -= 1;
                MaxSLagoon -= 1;
                SpecialLimit += 1;
            }
            else if (SpawnVal == 3)
            {
                SpawnVal = Random.Range(7, 11);//sets tile as a house tile
                CurHouse -= 1;
                MaxHouse -= 1;
                SpecialLimit += 1;
            }
            else if (SpawnVal == 4)
            {
                SpawnVal = Random.Range(4, 7);//set tiles as a water tile
                CurWater -= 1;
                MaxWater -= 1;
                SpecialLimit += 1;
            }
            if(CurHor == 0 || CurHor == 1)
            {
                if(VerticalPos == 5 || VerticalPos == 6 || VerticalPos == 7)
                {
                    SpawnVal = 0;
                }
            }
            Instantiate(Tiles[SpawnVal], SpawnPoint.position, SpawnPoint.rotation);
            LastSpawn = SpawnPoint;
            CurHor += 1;
            if (CurHor == HorizontalMax)//allows list to update and change position so that it will place tiles in the next horizontal line
            {
                SpecialLimit = 0;
                HorizontalPos = 0;
                VerticalPos += 1;
                CurHor = 0;
                SpawnPoint.position = new Vector2(0, VerticalPos);
            }
            else
            {
                HorizontalPos += 1;
                SpawnPoint.position = new Vector2(HorizontalPos, VerticalPos);
            }
            yield return new WaitForSeconds(.01f);
        }
    }
}
