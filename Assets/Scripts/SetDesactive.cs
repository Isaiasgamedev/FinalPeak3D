using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDesactive : MonoBehaviour
{
	public GameObject Desactive;

	public void SetDesactiveNow()
	{
		Desactive.SetActive(false);
	}
}
