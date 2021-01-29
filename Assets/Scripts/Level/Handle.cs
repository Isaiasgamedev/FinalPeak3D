using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour
{

    public Animator AnimControl;
    public Player x = null;
    public int ControlHandle;
    public Puzzle_01 Puzzle01;
    public Doors[] DoorOpen;
    public enum HandleType { Door, Plataform}
    public HandleType HandleTypeNow;
    public PlatMovelHorizontal Plat;  
    public bool HandleActive;



    private void OnTriggerStay(Collider other)   
    {
        x = other.GetComponent<Player>();
        if(HandleTypeNow == HandleType.Door)
        {
            if (x != null)
            {
                if (Input.GetButtonDown("Action"))
                {
                    AnimControl.Play("Active");
                    for (int i = 0; i < DoorOpen.Length; i++)
                    {
                        DoorOpen[i].OrbsControl = Doors.Orbs.ActiveDoor;
                    }
                    //Puzzle01 = GetComponentInParent<Puzzle_01>();
                    //Puzzle01.HandleActive = ControlHandle;
                    //Puzzle01.DoActionResolvePuzzle();
                    //Debug.Log("TESTE");
                }

            }
        }
        else if(HandleTypeNow == HandleType.Plataform)
        {
            if(Plat.StatePlatNow == PlatMovelHorizontal.StatePlat.ToWait)
            {
                if (Input.GetButtonDown("Action"))
                {
                    if (!HandleActive)
                    {
                        AnimControl.speed = 1;
                        AnimControl.Play("Active");
                        HandleActive = true;
                       
                        if (Plat.PlatTypeNow == PlatMovelHorizontal.PlatType.GoAndWait)
                        {
                            if (Plat.PlayerWas)
                            {
                                Plat.StatePlatNow = PlatMovelHorizontal.StatePlat.ToBack;

                            }
                            else
                            {
                                Plat.StatePlatNow = PlatMovelHorizontal.StatePlat.ToGO;
                            }
                        }

                        else
                        {
                            Plat.StatePlatNow = PlatMovelHorizontal.StatePlat.ToGO;
                        }
                        
                        

                        //Puzzle01 = GetComponentInParent<Puzzle_01>();
                        //Puzzle01.HandleActive = ControlHandle;
                        //Puzzle01.DoActionResolvePuzzle();
                        //Debug.Log("TESTE");
                    }

                }

                
            }
        }
        
    }

    public void DesactiveHandle()
    {
        AnimControl.Play("Desactive");
    }
}
