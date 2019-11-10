
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Grid : MonoBehaviour
{
    [SerializeField] private Transform hexagonPfb;
    [SerializeField] private int gridWth, gridHgt;
    [SerializeField] private float gap, hexagonWth, hexagonHgt, hexagonScl;
    private Vector3 startPos;
    private int hexagonsX, hexagonsY;


    // We initialize some variables and add the gap to the hexagons's with and height, we calculate the starting position of the grid, and we finally create it.
    private void Start ()
    {
        hexagonWth *= hexagonScl;
        hexagonHgt *= hexagonScl;
        hexagonsX = (int) (gridWth / hexagonWth);
        hexagonsY = (int) (gridHgt / hexagonHgt);

        AddGap ();
        StartPosition ();
        CreateGrid ();
    }


    //
    private void OnDrawGizmos ()
    {
        Gizmos.DrawWireCube (this.transform.position, new Vector3 (gridWth, 1, gridHgt));
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

        float x = -(gridWth / 2) + offset;
        float z = +0.5f * (gridHgt / 2);
        //float x = -hexagonWth * (gridWth / 2) - offset;
        //float z = +hexagonHgt * 0.75f * (gridHgt / 2);

        startPos = new Vector3 (x, 0.1f, z);
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

        return new Vector3 (x, 0.1f, z);
    }


    // We instantiate every hexagon and put it in its corresponding position on the grid.
    private void CreateGrid () 
    {
        int hexagonCnt = 1;

        for (int y = 0; y <= hexagonsY; y += 1) 
        {
            for (int x = 0; x <= hexagonsX; x += 1)
            {
                Transform hexagon = Instantiate (hexagonPfb) as Transform;
                Vector2 gridPos = new Vector2 (x, y);

                hexagon.position = CalculateWorldPosition (gridPos);
                hexagon.localScale *= hexagonScl;
                hexagon.parent = this.transform;
                hexagon.name = "Hexagon" + hexagonCnt;
                hexagonCnt += 1;
            }
        }

        Hexagon[] hexagons = this.GetComponentsInChildren<Hexagon> ();

        AssignNeighbours (hexagons);
    }


    private void AssignNeighbours (Hexagon[] hexagons)
    {
        int[] positions = new int[] {-hexagonsX - 2, -hexagonsX - 1, +1, +hexagonsX + 2, +hexagonsX + 1, -1};
        for (int h = 0; h < hexagons.Length; h += 1) 
        {
            int[] indices = new int[positions.Length];

            for (int p = 0; p < positions.Length; p += 1) 
            {
                if ((h + positions[p]) > -1 && (h + positions[p]) < hexagons.Length)
                {
                    indices[p] = h + positions[p];
                }
                else 
                {
                    indices[p] = -1;
                }
            }
            for (int i = 0; i < indices.Length; i += 1) 
            {
                if (indices[i] == -1)
                {
                    hexagons[h].neighbours[i] = null;
                }
                else 
                {
                    hexagons[h].neighbours[i] = hexagons[indices[i]];
                }
            }
        }
    }
}