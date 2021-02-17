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
			FlyeyeNow.AtttackSatesNow = FlyEye.AtttackSates.InReturn;
			if (x.StatesOfAttackNow != Player.StatesOfAttack.Inwait) return;
			StartCoroutine(x.KnockBack(-3));
			
		}
	}
}
