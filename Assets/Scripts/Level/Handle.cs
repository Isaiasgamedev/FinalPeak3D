using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour
{

    public Animator AnimControl;
    public PlayerMovement x = null;
    public int ControlHandle;
    public Puzzle_01 Puzzle01;


    private void OnTriggerStay(Collider other)   
    {
        x = other.GetComponent<PlayerMovement>();
        if (x != null)
        {
            if (Input.GetButtonDown("Action"))
            {
                AnimControl.Play("Active");
                Puzzle01 = GetComponentInParent<Puzzle_01>();
                Puzzle01.HandleActive = ControlHandle;
                Puzzle01.DoActionResolvePuzzle();
                Debug.Log("TESTE");
            }
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(x != null)
        {
            x = null;
        }
    }
}
