using UnityEngine;
using System.Collections;
using System;

[Serializable]

public class OrbsData
{
    public string OrbsName;
    public int Unique;
    public bool PlayerHas;    
}

public class OrbsDataPlayer : ScriptableObject
{   
    public OrbsData[] DataNow;    
}