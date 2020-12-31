using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbsLiberation : MonoBehaviour
{
    public enum Orbs {ActiveDoor, DesactiveDoor, RedOrb, GreenOrb, BlueOrb, PurpleOrb, OrangeOrb, YellowOrb, PinkOrb }
    public Orbs OrbsControl;
    public Animator AnimControl;

    private void OnTriggerEnter(Collider other)
    {
        var x = other.GetComponent<PlayerMovement>();
        if(x != null)
        {
            ManagerItens.Instance.OrbsDataNow.DataNow[(int)OrbsControl].PlayerHas = true;
            AnimControl.Play("Destroy");
        }
    }

    bool AnimatorIsPlaying()
    {
        return AnimControl.GetCurrentAnimatorStateInfo(0).length >
               AnimControl.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public void DestroyObject()
    {
        if (!AnimatorIsPlaying())
        {
            Destroy(this);
        }
        
    }
}
