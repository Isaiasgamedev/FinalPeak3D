using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPosition : MonoBehaviour
{

	public Transform ReturnPositionNow;

	private void OnTriggerStay(Collider other)
	{
		if (other)
		{
			if (other.GetComponent<Player>())
			{
				var x = other.GetComponent<Player>();
				if (x)
				{
					x.gameObject.transform.position = ReturnPositionNow.position;
				}
			}
		}
	}
	
}
