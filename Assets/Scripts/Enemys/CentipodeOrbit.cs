using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipodeOrbit : BaseEnemys
{
	[Header("CENTIPODE SETTINGS")]
	public GameObject cube;
	public Transform center;
	public Vector3 axis = Vector3.up;
	public Vector3 desiredPosition;
	public float radius = 2.0f;
	public float radiusSpeed = 0.5f;
	public float rotationSpeed = 80.0f;
	public GameObject DamageTextPrefab;



	// Start is called before the first frame update
	void Start()
	{		
		center = cube.transform;
		transform.position = (transform.position - center.position).normalized * radius + center.position;
		radius = 2.0f;
	}

	// Update is called once per frame
	void Update()
	{
		transform.RotateAround(center.position, axis, rotationSpeed * Time.deltaTime);
		desiredPosition = (transform.position - center.position).normalized * radius + center.position;
		transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
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
		for (int i = 0; i < DoorsActive.Length; i++)
		{
			DoorsActive[i].OrbsControl = Doors.Orbs.ActiveDoor;
		}
		Destroy(gameObject);
    }
}
