using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFollow : BaseEnemys
{

	[Header("SLIME SETTINGS")]
	
	public GameObject DamageTextPrefab;
	public Transform[] Points;
	public int PointsMore;
	public Animator Anim;
	public enum Actions {Sense, Attack, Move}
	public Actions ActionsNow;
	public float ActionTimer;
	public Transform PrincipalBody;
	public SlimeSense Sense;

	public void Update()
	{
		if(Sense.PlayerNow != null)
		{
			Player = Sense.PlayerNow;
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
		
		Anim.Play("RunFWD");
		PrincipalBody.position = Vector3.MoveTowards(PrincipalBody.position, Points[PointsMore].position, Time.deltaTime);
		PrincipalBody.LookAt(Points[PointsMore]);
		if (Vector3.Distance(PrincipalBody.position, Points[PointsMore].position) < 0.001f)
		{
			if (PointsMore < Points.Length - 1)
			{
				PointsMore++;
			}
			else
			{
				PointsMore = 0;
			}
		}

		if (Player != null)
		{
			ActionTimer += Time.deltaTime;
			if(ActionTimer > 3)
			{
				ActionsNow = Actions.Sense;
				ActionTimer = 0;
			}
		}
	}

	public void SensePlayer()
	{
		if (Player != null)
		{
			PrincipalBody.LookAt(Player.gameObject.transform);
		
			Anim.Play("SenseSomethingRPT");
			ActionTimer += Time.deltaTime;
			if (ActionTimer > 1.5f)
			{
				ActionsNow = Actions.Attack;
				ActionTimer = 0;
			}
		}
	

		else
		{
			ActionsNow = Actions.Move;
		}
		
	}

	public void AttackPlayer()
	{
		if (Player != null)
		{
			PrincipalBody.LookAt(Player.gameObject.transform);
			Anim.Play("Attack02");
			ActionTimer += Time.deltaTime;
			if (ActionTimer > 0.8f)
			{
				ActionsNow = Actions.Move;
				ActionTimer = 0;
			}
		}

		else
		{
			ActionsNow = Actions.Move;
		}
	}


	public override void DoDamage(int Dano)
	{
		if (Hp > 0)
		{
			Hp -= Dano;
			ShowDamage(Dano);
			if (Hp <= 0)
			{
				DoDestroy();
			}
		}
	}

	public void ShowDamage(float FinalDamage)
	{
		DamageTextPrefab.GetComponent<TextMesh>().text = FinalDamage.ToString();
		DamageTextPrefab.SetActive(true);
		DamageTextPrefab.GetComponent<Animator>().Play("DamageText");

	}

	public override void DoDestroy()
	{
		for (int i = 0; i < DoorsActive.Length; i++)
		{
			DoorsActive[i].OrbsControl = Doors.Orbs.ActiveDoor;
		}
		Destroy(gameObject);
	}


}
