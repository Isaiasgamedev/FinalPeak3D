using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemys : MonoBehaviour
{
	[Header("TYPE OF ENNEMY")]

	public TypeEnemy TypeEnemyNow;
	public enum TypeEnemy { Patrol, Follow, Trap, Orbit, Slime }
	public bool DamagePlayerNow;
	public Player Player = null;
	public Transform Target;
	public GameObject Bullet;
	public Doors[] DoorsActive;	

    [Header("ENNEMY STATUS")]

    public int Hp;
    public int Speed;
    public int Attack;
    public int Defense;
    public int Agility;
    public int Dextry;

    public virtual void DoAction()
    {
		

	}

    public virtual void DoAttack()
    {

    }

    public virtual void DoDamage(int Dano)
    {

    }

    public virtual void DoDestroy()
    {

    }

    public virtual void OnTriggerStay(Collider other)
    {
        //var x = other.GetComponent<Player>();
        //if (x)
        //{
        //    if (!DamagePlayerNow)
        //    {
        //        StartCoroutine(x.KnockBack(-3));
        //        DamagePlayerNow = true;
        //    }
                       
        //}
    }

    public void OnTriggerExit(Collider other)
    {
        //var x = other.GetComponent<Player>();
        //if (x)
        //{
        //    if (DamagePlayerNow)
        //    {
        //        DamagePlayerNow = false;
        //    }

        //}
    }
}
