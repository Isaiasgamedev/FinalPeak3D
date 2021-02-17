using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMinimaze : MonoBehaviour
{
	public Animator anim;

	public void PlayAnimNow()
	{
		anim.Play("Minimaze");
		
	}

	

	public void DestroyObject()
	{		
		Destroy(this.gameObject);		

	}
}
