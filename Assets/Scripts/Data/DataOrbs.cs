using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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