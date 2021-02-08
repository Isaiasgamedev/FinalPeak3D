using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCam : MonoBehaviour
{
	public GollemFlorest Gollem;


    public void ShakecamNow()
	{

		if (Gollem.StateBossNow == GollemFlorest.StateBoss.Second)
		{
			CinemachineShake.Instance.ShakeCamera(2.5f, .5f);
		}		
		
	}
}
