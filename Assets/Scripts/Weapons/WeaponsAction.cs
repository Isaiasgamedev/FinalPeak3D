﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsAction : MonoBehaviour
{
    public GameObject Reference;
    public enum TypesWeapon { Melle, Range, Magic, Explosive }
    public TypesWeapon TypesWeaponNow;
    public Animator AnimControl;


    private void OnTriggerEnter(Collider other)
    {
        var x = other.GetComponent<PlayerMovement>();
        if (x != null)
        {
            ManagerItens.Instance.WeaponDataNow.DataNow[(int)TypesWeaponNow].Level++;
            AnimControl.Play("Destroy");
        }
    }

    bool AnimatorIsPlaying()
    {
        return AnimControl.GetCurrentAnimatorStateInfo(0).length >
               AnimControl.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public void DestroyObject()
    {
        if (!AnimatorIsPlaying())
        {
            Destroy(Reference);
        }

    }

}
