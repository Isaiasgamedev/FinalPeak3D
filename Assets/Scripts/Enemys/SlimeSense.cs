using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSense : MonoBehaviour
{
	
	public Player PlayerNow = null;



	private void OnTriggerStay(Collider other)
	{
		var x = other.GetComponent<Player>();
		if(x != null)
		{			
			//Debug.Log("Player");
			PlayerNow = x;
			//Debug.Log(x.gameObject.name);
		}
		
	}

	private void OnTriggerExit(Collider other)
	{
		PlayerNow = null;
	}
}
