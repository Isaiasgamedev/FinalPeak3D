using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodamageEnemy : MonoBehaviour
{
	public FlyEye FlyeyeNow;

	private void OnTriggerEnter(Collider other)
	{
		var x = other.GetComponent<Player>();
		if (x)
		{			
			StartCoroutine(x.KnockBack(-3));
			FlyeyeNow.AtttackSatesNow = FlyEye.AtttackSates.InReturn;
		}
	}
}
