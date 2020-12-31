using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]

public class DataPuzzles
{
    public string PuzzleName;
    public int PuzzleLevel;
    public int PuzzleOrg;
    public bool PuzzleDone;
}

public class DataPuzzlesPlayer : ScriptableObject
{
    public DataPuzzles[] DataNow;
}
