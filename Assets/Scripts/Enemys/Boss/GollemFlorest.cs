using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GollemFlorest : BaseEnemys
{
	public Transform[] Points;
	public int PointsMore;
	public Transform PrincipalBody;
	public Animator Anim;
	public ParticleSystem Attack1;
	public SlimeSense Sense;
	public enum StateBoss {First, Second, Third, inWait }
	public StateBoss StateBossNow;
	public float TimerFollow;
	public int CountIdle;	
	public bool shakeCamNow;
	public bool ControlCount;
	public Transform[] Guardians;
	public GameObject[] GuardiansPrefab;

	void Update()
    {

		if (Sense.PlayerNow != null)
		{
			Player = Sense.PlayerNow;
		}

		switch (StateBossNow)
		{
			case StateBoss.First:
				MoveSlime();
				break;
			case StateBoss.Second:
				FollowPlayer();
				break;
			case StateBoss.Third:
				SummonGuardians();
				break;
		}

		if (shakeCamNow)
		{
			if (StateBossNow == StateBoss.Second)
			{
				CinemachineShake.Instance.ShakeCamera(5f, .5f);
			}
			shakeCamNow = false;
		}
	}


	public void MoveSlime()
	{
		//Anim.SetBool("Attack", false);
		LookPoints();
		PrincipalBody.transform.position = Vector3.MoveTowards(PrincipalBody.transform.position, Points[PointsMore].position, Time.deltaTime * 2);
		if (Vector3.Distance(PrincipalBody.position, Points[PointsMore].position) < 0.001f)
		{
			if (PointsMore < Points.Length - 1)
			{
				if (Player)
				{
					StartCoroutine(AttackPlayer1());
				}
				else
				{
					PointsMore++;
				}

			}
			else
			{
				if (Player)
				{
					StateBossNow = StateBoss.Second;
					Anim.SetBool("Attack", false);
					PointsMore = 0;
				}
				else
				{
					PointsMore = 0;
				}


			}
		}
	}

	public void FollowPlayer()
	{
		Anim.speed = 2f;
		LookPlayer();
		TimerFollow += Time.deltaTime;
		

		if (CountIdle > 2)
		{
			CountIdle = 0;
			TimerFollow = 0;
			StateBossNow = StateBoss.Third;
		}

		if(TimerFollow < 10)
		{
			PrincipalBody.position = Vector3.MoveTowards(PrincipalBody.position, Player.gameObject.transform.position, Time.deltaTime * 4);
			if (Vector3.Distance(PrincipalBody.position, Player.gameObject.transform.position) < 0.001f)
			{
				if (PointsMore < Points.Length - 1)
				{
					if (Player)
					{
						StartCoroutine(AttackPlayer2());

					}

				}
				else
				{
					PointsMore = 0;
				}
			}
		}

		else if(TimerFollow > 10 && TimerFollow < 15)
		{
			Anim.Play("Idle");
			if (ControlCount)
			{
				CountIdle++;
				ControlCount = false;
			}
			
		}

		else
		{
			TimerFollow = 0;
			Anim.Play("Walk");
			ControlCount = true;

		}

	}
	

	public void SummonGuardians()
	{
		Anim.speed = 2f;
		LookPoints();
		PrincipalBody.position = Vector3.MoveTowards(PrincipalBody.position, Points[0].position, Time.deltaTime * 4);
		if (Vector3.Distance(PrincipalBody.position, Points[0].position) < 0.001f)
		{
			if (PointsMore < Points.Length - 1)
			{
				//Anim.Play("Victory");
				SpawnGuardians();
			}
			
		}
	}

	public void SpawnGuardians()
	{
		for (int i = 0; i < GuardiansPrefab.Length; i++)
		{
			GameObject Guardian = Instantiate(GuardiansPrefab[i], Guardians[i].position, Quaternion.LookRotation(Guardians[i].forward), Guardians[i]);
			Guardian.SetActive(true);			
		}

		Anim.Play("GollemIdle");

		StateBossNow = StateBoss.inWait;
	}

	public void LookPoints()
	{
		Vector3 lookVector = Points[PointsMore].position - PrincipalBody.transform.position;
		lookVector.y = PrincipalBody.transform.position.y;
		Quaternion rot = Quaternion.LookRotation(lookVector);
		PrincipalBody.transform.rotation = Quaternion.Slerp(PrincipalBody.transform.rotation, rot, Time.deltaTime * 4);
		PrincipalBody.transform.localEulerAngles = new Vector3(0, PrincipalBody.transform.localEulerAngles.y, 0);
	}

	public void LookPlayer()
	{
		Vector3 lookVector = Player.gameObject.transform.position - PrincipalBody.transform.position;
		lookVector.y = PrincipalBody.transform.position.y;
		Quaternion rot = Quaternion.LookRotation(lookVector);
		PrincipalBody.transform.rotation = Quaternion.Slerp(PrincipalBody.transform.rotation, rot, Time.deltaTime * 4);
		PrincipalBody.transform.localEulerAngles = new Vector3(0, PrincipalBody.transform.localEulerAngles.y, 0);
	}


	public IEnumerator AttackPlayer1()
	{
		if (Player)
		{
			Anim.SetBool("Attack", true);
			AttackNow1();
		}
		
		yield return new WaitForSeconds(1.5f);
		if (Player)
		{
			Anim.SetBool("Attack", false);
		}
		
	}

	public void AttackNow1()
	{
		Attack1.gameObject.transform.position = Player.gameObject.transform.position;
		Attack1.playbackSpeed = 0.01f;
		Attack1.Play();
		PointsMore++;
	}


	public IEnumerator AttackPlayer2()
	{
		if (Player)
		{
			Anim.SetBool("Attack", true);
			AttackNow2();
		}

		yield return new WaitForSeconds(1.5f);
		if (Player)
		{
			Anim.SetBool("Attack", false);
			StateBossNow = StateBoss.First;
		}

	}

	public void AttackNow2()
	{
		Attack1.gameObject.transform.position = Player.gameObject.transform.position;
		Attack1.playbackSpeed = 0.01f;
		Attack1.Play();
		PointsMore++;
	}


}
