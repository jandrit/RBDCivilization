
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


    //
    public List<Transform> GetPath (Hexagon hex) 
    {
        float checkedDst;

        int bestChoice = 0;
        float bestDst = Vector3.Distance (this.transform.position, hex.transform.position);
        List<Transform> result = new List<Transform> ();
        Hexagon currentHex = this;

        while (bestDst != 0) 
        {
            for (int i = 0; i < currentHex.neighbours.Length; i += 1)
            {
                if (currentHex.neighbours[i] != null)
                {
                    checkedDst = Vector3.Distance (currentHex.neighbours[i].transform.position, hex.transform.position);
                    print(checkedDst);
                    if (checkedDst < bestDst)
                    {
                        bestChoice = i;
                        bestDst = checkedDst;

                        /*if (bestDst == 0) 
                        {
                            break;
                        }*/
                    }
                }
            }

            result.Add (currentHex.neighbours[bestChoice].transform);

            currentHex = currentHex.neighbours[bestChoice];
            print(currentHex.name);
        }

        return result;
    }
}