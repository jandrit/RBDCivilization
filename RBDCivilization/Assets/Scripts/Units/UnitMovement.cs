
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UnitMovement : MonoBehaviour
{
    public Transform target;
    public bool reachedTrg;

    [SerializeField] private int moveSpd;
    private CharacterController characterCtr;
    private Hexagon currentHex;


    // Start is called before the first frame update.
    private void Start ()
    {
        reachedTrg = false;
        characterCtr = this.GetComponent<CharacterController> ();
    }


    // Update is called once per frame.
    private void Update ()
    {
        while (target == null) 
        {
            target = GameObject.FindGameObjectWithTag("Hexagon").transform;
        }

        if (reachedTrg == false) 
        {
            characterCtr.Move ((target.position - this.transform.position).normalized * moveSpd * Time.deltaTime);

            if (Vector3.Distance (this.transform.position, target.position) < 2)
            {
                reachedTrg = true;
                currentHex.AddUnit (this);
            }
        }
    }


    //
    private void OnTriggerEnter (Collider other)
    {
        currentHex = other.GetComponent<Hexagon> ();
    }
}