using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public Material[] DoorMats;
    public Renderer[] MeshControl;
    public Animator[] AnimatorControl;
    public bool StayOpen;
    public bool OutOfTrigger;

    public enum Orbs {ActiveDoor, DesactiveDoor, RedOrb, GreenOrb, BlueOrb, PurpleOrb, OrangeOrb, YellowOrb, PinkOrb}
    public Orbs OrbsControl;


    private void Update()
    {
        for (int i = 0; i < MeshControl.Length; i++)
        {
            MeshControl[i].material = DoorMats[(int)OrbsControl];
        }
    }


    bool AnimatorIsPlaying()
    {
        return AnimatorControl[0].GetCurrentAnimatorStateInfo(0).length >
               AnimatorControl[0].GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (ManagerItens.Instance.OrbsDataNow.DataNow[(int)OrbsControl].PlayerHas)
        {
            for (int i = 0; i < AnimatorControl.Length; i++)
            {
                AnimatorControl[i].SetBool("Close", false);
                AnimatorControl[i].Play("Doors");
                if (!AnimatorIsPlaying())
                {
                    StayOpen = true;
                }

            }
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (ManagerItens.Instance.OrbsDataNow.DataNow[(int)OrbsControl].PlayerHas)
        {
            if (StayOpen)
            {
                for (int i = 0; i < AnimatorControl.Length; i++)
                {
                    AnimatorControl[i].SetBool("Close", true);
                    //AnimatorControl[i].Play("Reverse");
                }
            }
            else
            {
                OutOfTrigger = true;
                StartCoroutine(CloseDoors());
            }
        }
    }

    IEnumerator CloseDoors()
    {
        if (StayOpen)
        {
            yield break;
        }
        yield return new WaitForSeconds(10);

        //After we have waited 5 seconds print the time again.

        for (int i = 0; i < AnimatorControl.Length; i++)
        {
            AnimatorControl[i].SetBool("Close", true);
            //AnimatorControl[i].Play("Reverse");
        }
        
    }


}
