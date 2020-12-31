using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]

public class DataWeapons
{
    public string WeaponName;
    public enum TypesWeapon { Melle, Range, Magic, Explosive}
    public TypesWeapon TypesWeaponNow;
    public int Level;
}

public class DataWeaponsPlayer : ScriptableObject
{
    public DataWeapons[] DataNow;
}