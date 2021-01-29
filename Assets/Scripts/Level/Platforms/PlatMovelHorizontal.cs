using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatMovelHorizontal : MonoBehaviour
{
    public Transform[] Targets;
    public Vector3 TargetNow;
    public int ControlIndexTarget;
    public enum PlatType {GoAndBack, GoAndWait}
    public PlatType PlatTypeNow;
    public enum StatePlat { ToGO, ToBack, ToWait}
    public StatePlat StatePlatNow;
    public float Speed;
    public Player Player;
    public GameObject Parent;
    public bool PlayerWas;
    public Handle HandleState;

    private void FixedUpdate()   
    {
        switch (PlatTypeNow)
        {
            case PlatType.GoAndBack:
                TargetNow = Targets[ControlIndexTarget].position;
                PlayerWas = false;
                if (StatePlatNow == StatePlat.ToGO)
                {
                    transform.position = Vector3.MoveTowards(transform.position, TargetNow, Speed * Time.deltaTime);

                    if (transform.position == Targets[ControlIndexTarget].position)
                    {
                        if (!Targets[ControlIndexTarget].GetComponent<PointsChangeDirection>().Final)
                        {
                            ControlIndexTarget++;
                        }
                        else
                        {
                            StatePlatNow = StatePlat.ToBack;
                            ControlIndexTarget--;
                        }
                    }
                }

                else if (StatePlatNow == StatePlat.ToBack)
                {
                    transform.position = Vector3.MoveTowards(transform.position, TargetNow, Speed * Time.deltaTime);

                    if (transform.position == Targets[ControlIndexTarget].position)
                    {
                        if (!Targets[ControlIndexTarget].GetComponent<PointsChangeDirection>().Start)
                        {
                            ControlIndexTarget--;
                        }
                        else
                        {
                            StatePlatNow = StatePlat.ToWait;
                            HandleState.DesactiveHandle();
                        }
                    }
                }
                break;

            case PlatType.GoAndWait:
                TargetNow = Targets[ControlIndexTarget].position;

                if (StatePlatNow == StatePlat.ToGO)
                {
                    transform.position = Vector3.MoveTowards(transform.position, TargetNow, Speed * Time.deltaTime);

                    if (transform.position == Targets[ControlIndexTarget].position)
                    {
                        if (!Targets[ControlIndexTarget].GetComponent<PointsChangeDirection>().Final)
                        {
                            ControlIndexTarget++;
                            PlayerWas = false;
                        }
                        else
                        {
                            PlayerWas = true;
                            StatePlatNow = StatePlat.ToWait;
                            ControlIndexTarget--;
                            HandleState.DesactiveHandle();
                        }
                    }
                }

                else if (StatePlatNow == StatePlat.ToBack)
                {
                    transform.position = Vector3.MoveTowards(transform.position, TargetNow, Speed * Time.deltaTime);

                    if (transform.position == Targets[ControlIndexTarget].position)
                    {
                        if (!Targets[ControlIndexTarget].GetComponent<PointsChangeDirection>().Start)
                        {
                            ControlIndexTarget--;                            
                        }
                        else
                        {
                            StatePlatNow = StatePlat.ToWait;
                            ControlIndexTarget++;
                            PlayerWas = false;
                            HandleState.DesactiveHandle();
                        }
                    }
                }

                break;
        }
           
        
    }

    

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        Player = other.GetComponent<Player>();
        if (Player)
        {
            Player.gameObject.transform.parent = Parent.transform;
            //if (StatePlatNow != StatePlat.ToGO)
            //{
            //    if (!PlayerWas)
            //    {
            //        StatePlatNow = StatePlat.ToGO;
            //    }
            //    else
            //    {
            //        StatePlatNow = StatePlat.ToBack;
            //    }

            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var Player = other.GetComponent<Player>();
        if (Player)
        {
            Player.gameObject.transform.parent = null;
        }
    }
}
