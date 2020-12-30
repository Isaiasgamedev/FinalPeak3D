using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : BaseEnemys
{
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
            if(Hp <= 0)
            {
                DoDestroy();                
            }
        }        
    }

    public override void DoDestroy()
    {
        Destroy(gameObject);
    }
}
