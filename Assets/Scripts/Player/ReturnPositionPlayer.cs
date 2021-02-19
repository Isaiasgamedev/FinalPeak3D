using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPositionPlayer : MonoBehaviour
{
	public Transform Reposition;

	private void OnTriggerStay(Collider other)
	{
		var x = other.GetComponent<Player>();
		if(x != null)
		{
			Debug.Log("Teste");
			x.transform.position = Reposition.position;
		}
	}



	
}
