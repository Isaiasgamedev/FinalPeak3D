using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEye : BaseEnemys
{

	[Header("FLYEYE SETTINGS")]
	public Transform PrincipalBody;
	public GameObject DamageTextPrefab;	
	public Animator Anim;
	public enum Actions { Sense, Attack, Move }
	public Actions ActionsNow;
	public enum AtttackSates { InAttack, InReturn, InWait}
	public AtttackSates AtttackSatesNow;
	public float ActionTimer;	
	public SlimeSense Sense;
	public Transform[] Eye;
	public Transform Prev;
	public float SpeedEye;
	public float Actime;
	float step;
	bool Look;
	public float turnRate;
	


	// Update is called once per frame
	public void Update()
	{
		step = Time.deltaTime * SpeedEye;
		if (Sense.PlayerNow != null)
		{
			Player = Sense.PlayerNow;
		}
		else
		{
			Player = null;
		}

		switch (ActionsNow)
		{
			case Actions.Move:
				MoveSlime();
				break;

			case Actions.Sense:
				SensePlayer();
				break;

			case Actions.Attack:
				AttackPlayer();
				break;
		}

	}


	public void MoveSlime()
	{
		
		if (Player != null)
		{
			LookPlayer();
		}
		

		if (Player != null)
		{
			ActionTimer += Time.deltaTime;
			if (ActionTimer > 3)
			{
				Anim.SetInteger("Control", 1);
				Anim.SetBool("Hit", false);
				ActionsNow = Actions.Sense;
				ActionTimer = 0;
				Look = true;
			}
		}
	}

	public void SensePlayer()
	{
		if (Player != null)
		{
			LookPlayer();

			
			ActionTimer += Time.deltaTime;
			if (ActionTimer > 1.5f)
			{
				Anim.SetInteger("Control", 2);
				Anim.SetBool("Hit", false);
				ActionsNow = Actions.Attack;
				AtttackSatesNow = AtttackSates.InAttack;
				ActionTimer = 0;
				Look = true;
			}
		}


		else
		{
			ActionsNow = Actions.Move;
		}

	}

	public void AttackPlayer()
	{
		
		Eye[0].transform.localEulerAngles = new Vector3(-90, 0, 0);
		if (Player != null && !DamagePlayerNow)
		{
			
			ActionTimer += Time.deltaTime;
			if (ActionTimer < Actime)
			{
									
				if(AtttackSatesNow == AtttackSates.InAttack)
				{
					LookPlayer();
					Eye[0].transform.position = Vector3.MoveTowards(Eye[0].transform.position, Player.gameObject.transform.position, step);
					Eye[0].LookAt(Player.gameObject.transform);
					Eye[0].transform.parent = this.gameObject.transform;
				}					
				
				if (Vector3.Distance(Eye[0].position, Player.gameObject.transform.position) < 0.1f)
				{
					AtttackSatesNow = AtttackSates.InReturn;					
				}

				if (AtttackSatesNow == AtttackSates.InReturn)
				{
					Eye[0].transform.position = Vector3.MoveTowards(Eye[0].transform.position, Eye[1].transform.position, step);
					Eye[0].LookAt(Player.gameObject.transform);
					if (Vector3.Distance(Eye[0].position, Eye[1].position) < 0.001f)
					{
						Anim.SetInteger("Control", 0);
						Anim.SetBool("Hit", false);
						Eye[0].transform.parent = Prev;
						ActionTimer = 0;
						ActionsNow = Actions.Move;
						AtttackSatesNow = AtttackSates.InWait;
						Look = true;

					}
				}

			}

			if (ActionTimer > Actime)
			{
				Eye[0].transform.position = Vector3.MoveTowards(Eye[0].transform.position, Eye[1].transform.position, step);
				Eye[0].LookAt(Player.gameObject.transform);
				if (Vector3.Distance(Eye[0].position, Eye[1].position) < 0.001f)
				{
					Anim.SetInteger("Control", 0);
					Anim.SetBool("Hit", false);
					Eye[0].transform.parent = Prev;
					ActionTimer = 0;
					ActionsNow = Actions.Move;
					AtttackSatesNow = AtttackSates.InWait;
					Look = true;

				}
			}




			//if (Vector3.Distance(Eye[0].position, Player.gameObject.transform.position) < 0.001f)
			//{
			//	Debug.Log("TESTE");
			//	Eye[0].transform.position = Vector3.MoveTowards(Eye[0].transform.position, Eye[1].transform.position, step);
			//	Eye[0].LookAt(Player.gameObject.transform);
			//	if (Vector3.Distance(Eye[0].position, Eye[1].position) < 0.001f)
			//	{
			//		Anim.SetInteger("Control", 0);
			//		Anim.SetBool("Hit", false);
			//		Eye[0].transform.parent = Prev;
			//		ActionTimer = 0;
			//		ActionsNow = Actions.Move;
			//		Look = true;
			//	}
			//}



			//if (ActionTimer > Actime)
			//{
			//	Eye[0].transform.position = Vector3.MoveTowards(Eye[0].transform.position, Eye[1].transform.position, step);
			//	Eye[0].LookAt(Player.gameObject.transform);
			//	if (Vector3.Distance(Eye[0].position, Eye[1].position) < 0.001f)
			//	{
			//		Anim.SetInteger("Control", 0);
			//		Anim.SetBool("Hit", false);
			//		Eye[0].transform.parent = Prev;
			//		ActionTimer = 0;
			//		ActionsNow = Actions.Move;
			//		Look = true;
			//	}
			//}


		}

		else
		{
			Debug.Log("ELSE");
			Eye[0].transform.position = Vector3.MoveTowards(Eye[0].transform.position, Eye[1].transform.position, step);			
			AtttackSatesNow = AtttackSates.InReturn;
			if (Vector3.Distance(Eye[0].position, Eye[1].position) < 0.001f)
			{
				Anim.SetInteger("Control", 0);
				Anim.SetBool("Hit", false);
				Eye[0].transform.parent = Prev;
				ActionTimer = 0;
				ActionsNow = Actions.Move;
				AtttackSatesNow = AtttackSates.InWait;
				Look = true;

			}
		}




	}

	

	public void LookPlayer()
	{
		Vector3 lookVector = Player.gameObject.transform.position - PrincipalBody.transform.position;
		lookVector.y = PrincipalBody.transform.position.y;
		Quaternion rot = Quaternion.LookRotation(lookVector);
		PrincipalBody.transform.rotation = Quaternion.Slerp(PrincipalBody.transform.rotation, rot, 1);
		PrincipalBody.transform.localEulerAngles = new Vector3(0, PrincipalBody.transform.localEulerAngles.y, PrincipalBody.transform.localEulerAngles.z);
	}
	

	public override void DoDestroy()
	{
		for (int i = 0; i < DoorsActive.Length; i++)
		{
			DoorsActive[i].OrbsControl = Doors.Orbs.ActiveDoor;
		}
		Destroy(PrincipalBody.gameObject);
	}


	public override void DoDamage(int Dano)
	{
		if (Hp > 0)
		{
			Anim.SetBool("Hit", true);
			Hp -= Dano;
			ShowDamage(Dano);
			//Anim.SetBool("Hit", false);
			if (Hp <= 0)
			{
				DoDestroy();
			}
		}
		if (Hp <= 0)
		{
			DoDestroy();
		}
	}

	public void ShowDamage(float FinalDamage)
	{
		DamageTextPrefab.GetComponent<TextMesh>().text = FinalDamage.ToString();
		DamageTextPrefab.SetActive(true);
		DamageTextPrefab.GetComponent<Animator>().Play("DamageText");
	}

}
