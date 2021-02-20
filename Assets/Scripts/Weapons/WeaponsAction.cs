using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsAction : MonoBehaviour
{
    public GameObject Reference;
    public enum TypesWeapon { Melle, Range, Magic, Explosive }
    public TypesWeapon TypesWeaponNow;
    public Animator AnimControl;
    public Puzzle_02 PuzzleResolve;
	public GameObject[] Enemy;
	public Doors[] Desactive;
	public ShieldMinimaze Play;


	private void OnTriggerEnter(Collider other)
    {
        var x = other.GetComponent<Player>();
        if (x != null)
        {
            ManagerItens.Instance.WeaponDataNow.DataNow[(int)TypesWeaponNow].Level++;
			//PuzzleResolve = GetComponentInParent<Puzzle_02>();
			//PuzzleResolve.DoActionResolvePuzzle();
			for (int i = 0; i < Enemy.Length; i++)
			{
				Enemy[i].SetActive(true);
			}
			
			for (int i = 0; i < Desactive.Length; i++)
			{
				Desactive[i].OrbsControl = Doors.Orbs.DesactiveDoor;
			}

			AnimControl.Play("Get");			

		}
    }

   

    public void DestroyObject()
    {        
		Play.PlayAnimNow();
		Destroy(Reference);
    }

}
