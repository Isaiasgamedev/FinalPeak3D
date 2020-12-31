using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_01 : BasePuzzle
{ 
    public GameObject[] PuzzlesNeeds;
    public int HandleActive;  


    public override void DoActionResolvePuzzle()
    {
        if (HandleActive == 1)
        {
            PuzzlesNeeds[0].SetActive(true);
            PuzzlesNeeds[3].SetActive(false);
        }
        else if (HandleActive == 2)
        {
            PuzzlesNeeds[2].SetActive(true);
            PuzzlesNeeds[1].SetActive(false);
        }
    }
}
