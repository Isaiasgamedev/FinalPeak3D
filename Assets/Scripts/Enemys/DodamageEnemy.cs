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
			if (FlyeyeNow)
			{
				FlyeyeNow.AtttackSatesNow = FlyEye.AtttackSates.InReturn;
			}
			
			if (!x.IndamageNow)
			{
				x.KnockBack(-3);
			}
			
		}
	}
}
