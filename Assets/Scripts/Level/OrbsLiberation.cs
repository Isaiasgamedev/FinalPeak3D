using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrbsLiberation : MonoBehaviour
{
    public enum Orbs {ActiveDoor, DesactiveDoor, RedOrb, GreenOrb, BlueOrb, PurpleOrb, OrangeOrb, YellowOrb, PinkOrb }
    public Orbs OrbsControl;    
	public Doors[] Desactive;
	public Animator anim;
	public Player x = null;
	public GameObject Particule;
	
	

	private void OnTriggerStay(Collider other)
    {

		x = other.GetComponent<Player>();	
		
		if (x != null)
		{
			if (Input.GetButtonDown("Action"))
			{
				//Debug.Log("Action");
				ManagerItens.Instance.OrbsDataNow.DataNow[(int)OrbsControl].PlayerHas = true;
				for (int i = 0; i < Desactive.Length; i++)
				{
					Desactive[i].OrbsControl = Doors.Orbs.DesactiveDoor;
				}

				anim.Play("RedConsole");
				Particule.SetActive(false);				
			}

		}		
    }

	private void OnTriggerExit(Collider other)
	{
		x = null;
	}
}
