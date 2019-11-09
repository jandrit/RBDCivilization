
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Grid : MonoBehaviour
{
    [SerializeField] private Transform hexagonPfb, terrainTrf;
    [SerializeField] private float gridWth, gridHgt, gap;
    private float hexagonWth, hexagonHgt;
    private Vector3 startPos;


    // We initialize some variables and add the gap to the hexagons's with and height, we calculate the starting position of the grid, and we finally create it.
    private void Start ()
    {
        gridWth = terrainTrf.localScale.x * 2;
        gridHgt = terrainTrf.localScale.z * 2;
        hexagonWth = hexagonPfb.localScale.x;
        hexagonHgt = hexagonPfb.localScale.z;

        AddGap ();
        StartPosition ();
        CreateGrid ();
    }


    // We add the gap value we've set to the hexagons's with and height.
    private void AddGap () 
    {
        hexagonWth += hexagonWth * gap;
        hexagonHgt += hexagonHgt * gap;
    }


    // We define the starting position where the first hexagon will be placed.
    private void StartPosition () 
    {
        float offset = 0;

        if (gridHgt / 2 % 2 != 0) 
        {
            offset = hexagonWth / 2;
        }

        float x = -hexagonWth * (gridWth / 2) - offset;
        float z = +hexagonHgt * 0.75f * (gridHgt / 2);

        startPos = new Vector3 (x, 0, z);
    }


    // Get an hexagon's world position from its position on the grid.
    Vector3 CalculateWorldPosition (Vector2 gridPos) 
    {
        float offset = 0;
        if (gridPos.y % 2 != 0) 
        {
            offset = hexagonWth / 2;
        }

        float x = startPos.x + gridPos.x * hexagonWth + offset;
        float z = startPos.z - gridPos.y * hexagonHgt * 0.75f;

        return new Vector3 (x, 0, z);
    }


    // We instantiate every hexagon and put it in its corresponding position on the grid.
    private void CreateGrid () 
    {
        int hexagonCnt = 1;

        for (int y = 0; y < gridHgt; y += 1) 
        {
            for (int x = 0; x < gridWth; x += 1)
            {
                Transform hexagon = Instantiate (hexagonPfb) as Transform;
                Vector2 gridPos = new Vector2 (x, y);

                hexagon.position = CalculateWorldPosition (gridPos);
                hexagon.parent = this.transform;
                hexagon.name = "Hexagon" + hexagonCnt;
                hexagonCnt += 1;
            }
        }
    }
}