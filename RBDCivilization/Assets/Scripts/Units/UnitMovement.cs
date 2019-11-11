
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UnitMovement : MonoBehaviour
{
    public Vector3 target;
    public bool reachedTrg;
    public Hexagon currentHex;

    [SerializeField] private int moveSpd;
    //[SerializeField] private UnitSettings stats;
    private CharacterController characterCtr;
    private Transform feet;
    private List<Vector3> path;
    private float offsetHexX, offsetHexZ;


    // Start is called before the first frame update.
    private void Start ()
    {
        Grid grid = GameObject.FindObjectOfType<Grid> ();

        reachedTrg = false;
        characterCtr = this.GetComponent<CharacterController> ();
        feet = this.transform.GetChild (0);
        path = new List<Vector3> ();
        target = GameObject.FindGameObjectWithTag("Hexagon").transform.position;
        offsetHexX = grid.hexagonWth / 4;
        offsetHexZ = grid.hexagonHgt / 4;

        path.Add (target);
    }


    // Update is called once per frame.
    private void Update ()
    {
        if (reachedTrg == false) 
        {
            characterCtr.Move ((target - this.transform.position).normalized * moveSpd * Time.deltaTime);
            if (Vector3.Distance (feet.position, target) < 0.5f)
            {
                path.RemoveAt (0);

                if (path.Count == 0)
                {
                    reachedTrg = true;

                    currentHex.AddUnit (this);
                }
                else 
                {
                    target = path[0];
                }
            }
        }
    }


    //
    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Hexagon")
        {
            currentHex = other.GetComponent<Hexagon> ();
        }
    }


    //
    public void FindPathTo (Hexagon hex) 
    {
        UnitMovement[] unitsHex = this.currentHex.UnitsPlaced ();

        path = this.currentHex.GetPath (hex);

        for (int u = 0; u < unitsHex.Length; u += 1) 
        {
            if (u != 0)
            {
                Vector3 offset;

                List<Vector3> pathAux = new List<Vector3> ();

                switch (u)
                {
                    case 1:
                        offset = new Vector3 (+offsetHexX, 0, +offsetHexZ);

                        break;
                    case 2:
                        offset = new Vector3 (-offsetHexX, 0, +offsetHexZ);

                        break;
                    case 3:
                        offset = new Vector3 (+offsetHexX, 0, -offsetHexZ);

                        break;
                    case 4:
                        offset = new Vector3 (-offsetHexX, 0, -offsetHexZ);

                        break;
                }


            }
            unitsHex[u].target = path[0];
            unitsHex[u].reachedTrg = false;
        }
    }
}