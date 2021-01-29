using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbsLiberation : MonoBehaviour
{
    public enum Orbs {ActiveDoor, DesactiveDoor, RedOrb, GreenOrb, BlueOrb, PurpleOrb, OrangeOrb, YellowOrb, PinkOrb }
    public Orbs OrbsControl;
    public Animator AnimControl;
	public Doors[] Desactive;

    private void OnTriggerEnter(Collider other)
    {
        var x = other.GetComponent<Player>();
        if(x != null)
        {
            ManagerItens.Instance.OrbsDataNow.DataNow[(int)OrbsControl].PlayerHas = true;
			for (int i = 0; i < Desactive.Length; i++)
			{
				Desactive[i].OrbsControl = Doors.Orbs.DesactiveDoor;
			}
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
