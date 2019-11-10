﻿
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
    private Transform feet;
    private List<Transform> path;


    // Start is called before the first frame update.
    private void Start ()
    {
        reachedTrg = false;
        characterCtr = this.GetComponent<CharacterController> ();
        feet = this.transform.GetChild (0);
        path = new List<Transform> ();
    }


    // Update is called once per frame.
    private void Update ()
    {
        while (target == null) 
        {
            target = GameObject.FindGameObjectWithTag("Hexagon").transform;
            path.Add (target);
        }

        if (reachedTrg == false) 
        {
            characterCtr.Move ((target.position - this.transform.position).normalized * moveSpd * Time.deltaTime);

            if (Vector3.Distance (feet.position, target.position) < 0.5f)
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


    public void FindPathTo (Hexagon hex) 
    {
        //path = currentHex.GetPath (hex);
    }
}