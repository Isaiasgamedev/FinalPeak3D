using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemys : MonoBehaviour
{
    [Header("TYPE OF ENNEMY")]

    public TypeEnemy TypeEnemyNow;
    public enum TypeEnemy {Patrol, Follow, Trap}

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
}
