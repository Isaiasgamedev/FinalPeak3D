using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodamageEnemy : MonoBehaviour
{
	
	private void OnTriggerEnter(Collider other)
	{
		var x = other.GetComponent<Player>();
		if (x)
		{			
			StartCoroutine(x.KnockBack(-3));
			
		}
	}
}
