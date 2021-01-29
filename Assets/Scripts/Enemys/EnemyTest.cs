using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : BaseEnemys
{
    public GameObject DamageTextPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void DoDamage(int Dano)
    {
        if(Hp > 0)
        {
            Hp -= Dano;
            ShowDamage(Dano);
            if (Hp <= 0)
            {
                DoDestroy();                
            }
        }        
    }

    public void ShowDamage(float FinalDamage)
    {
        DamageTextPrefab.GetComponent<TextMesh>().text = FinalDamage.ToString();
        DamageTextPrefab.SetActive(true);
        DamageTextPrefab.GetComponent<Animator>().Play("DamageText"); 

    }

    public override void DoDestroy()
    {
        Destroy(gameObject);
    }
}
