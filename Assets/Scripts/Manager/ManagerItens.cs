using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ManagerItens : MonoBehaviour
{
    protected static ManagerItens instance;
    public static ManagerItens Instance
    {
        get
        {
            if (ManagerItens.instance == null)
            {
                GameObject g = new GameObject("_ManagerItens");
                ManagerItens.instance = g.AddComponent<ManagerItens>();
                DontDestroyOnLoad(g);
            }
            return ManagerItens.instance;
        }
    }

    public OrbsDataPlayer OrbsDataNow;
    public DataWeaponsPlayer WeaponDataNow;


    private void Awake()
    {
        if (ManagerItens.instance != null && ManagerItens.instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
