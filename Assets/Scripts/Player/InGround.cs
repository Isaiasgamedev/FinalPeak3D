using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGround : MonoBehaviour
{
	public bool InGroundNow;

	private void OnTriggerStay(Collider other)
	{
		if (other)
		{
			if(other.gameObject.layer == 8)
			{
				InGroundNow = true;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		InGroundNow = false;
	}
}
