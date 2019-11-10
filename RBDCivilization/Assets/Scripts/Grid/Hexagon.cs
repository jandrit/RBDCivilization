
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Hexagon : MonoBehaviour
{
    public Hexagon[] neighbours;
    public UnitMovement[] units;

    private int presentUnt;


    // .
    private void Awake ()
    {
        neighbours = new Hexagon[6];
        units = new UnitMovement[5];
        presentUnt = 0;
    }


    // Update is called once per frame.
    private void Update ()
    {
        
    }


    //
    private void OnTriggerExit (Collider other)
    {
        if (other.tag == "Unit") 
        {
            units = new UnitMovement[5];
            presentUnt = 0;
        }
    }


    //
    public void AddUnit (UnitMovement unit) 
    {
        units[presentUnt] = unit;
        presentUnt += 1;
    }


    //
    public int GetCapacity () 
    {
        return (units.Length - presentUnt);
    }


    //
    public UnitMovement[] UnitsPlaced () 
    {
        if (presentUnt == 0)
        {
            return null;
        }
        else 
        {
            UnitMovement[] result = new UnitMovement[presentUnt];

            for (int u = 0; u < result.Length; u += 1)
            {
                result[u] = units[u];
            }

            return result;
        }
    }
}