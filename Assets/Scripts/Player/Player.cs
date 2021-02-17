using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player: MonoBehaviour
{
    [Header("INITIAL CONTROLLERS PLAYER")]

    CharacterController controller;
    public Vector3 move;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float gravityValue = -9.81f; 
    public float TimerKnockBack;
    public Animator AnimControl;
    public GameObject DamageTextPrefab;
	public TextMeshProUGUI HpValor;

    [Header("STATUS OF PLAYER")]

    //public float RotPlayer;
    public float Hp;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public int Attack;
    public int Defense;
    public int Agility;
    public int Dextry;


    [Header("STATES OF ATTACK")]
    public StatesOfAttack StatesOfAttackNow;
    public enum StatesOfAttack { Inwait, InAttack, InKnockBack, Indamage}    
    public int ComboControl;
    public GameObject WeaponInUseNow;
    public GameObject MelleAttack;
    public bool Indialogue;
	public float TimerDamage;
	public bool IndamageNow;
	public GameObject WeaponNow;



	private void Start()
    {
        controller = GetComponent<CharacterController>();    
    }

    void Update()
    {
		MovePlayer();
		AttackPlayer();
		AnimationsControl();
		ReturnToNormal();
	}

	public void AnimationsControl()
	{
		if (Indialogue)
		{
			AnimControl.SetInteger("ControlAnim", 0);
		}

		if(StatesOfAttackNow == StatesOfAttack.InAttack)
		{
			AnimControl.SetInteger("ControlAnim", 2);
		}
		
	}

	public void MovePlayer()
	{
		
		if (StatesOfAttackNow == StatesOfAttack.InAttack) return;
		if (StatesOfAttackNow == StatesOfAttack.InKnockBack) return;
		if (Indialogue) return;


		groundedPlayer = controller.isGrounded;
		if (groundedPlayer && playerVelocity.y < 0)
		{
			playerVelocity.y = 0f;
		}

		move = new Vector3(0, 0, Input.GetAxis("Vertical"));


		if (move.z > 0)
		{
			controller.Move(transform.forward * Time.deltaTime * playerSpeed);
			AnimControl.SetInteger("ControlAnim", 1);
		}
		else if (move.z < 0)
		{
			controller.Move(transform.forward * Time.deltaTime * -playerSpeed);
			AnimControl.SetInteger("ControlAnim", 1);
		}		

		if (Input.GetAxis("Horizontal") < 0)
		{
			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + Input.GetAxis("Horizontal") * Time.deltaTime * 64, 0);
			if (move.z == 0)
			{
				AnimControl.SetInteger("ControlAnim", 0);
			}
		}
		else if (Input.GetAxis("Horizontal") > 0)
		{
			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + Input.GetAxis("Horizontal") * Time.deltaTime * 64, 0);
			if (move.z == 0)
			{
				AnimControl.SetInteger("ControlAnim", 0);
			}
		}

		// Changes the height position of the player..
		if (Input.GetButtonDown("Jump") && groundedPlayer)
		{
			AnimControl.Play("Jump");
			playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
		}

		if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
		{
			AnimControl.SetInteger("ControlAnim", 0);
		}

		playerVelocity.y += gravityValue * Time.deltaTime;
		controller.Move(playerVelocity * Time.deltaTime);
	}

	

	public void AttackPlayer()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			
			if (ManagerItens.Instance.WeaponDataNow.DataNow[0].Level > 0)
			{
				if (StatesOfAttackNow == StatesOfAttack.Inwait)
				{
					ComboControl = 0;
					if(WeaponNow == null)
					{
						WeaponNow = Instantiate(MelleAttack, WeaponInUseNow.transform.position, Quaternion.identity, WeaponInUseNow.transform);
						WeaponNow.transform.localEulerAngles = transform.forward;
						WeaponNow.GetComponentInChildren<Sword>().Pm = this;
						WeaponNow.GetComponentInChildren<Sword>().Damage += CalculateAttack(0);
						StatesOfAttackNow = StatesOfAttack.InAttack;
					}
				}
				else
				{
					ComboControl++;
				}
			}


		}
	}

    public void Dodamage(int Dano)
    {		
		CinemachineShake.Instance.ShakeCamera(4f, .1f);
		if (WeaponNow)
		{
			Destroy(WeaponNow);
		}
		StatesOfAttackNow = StatesOfAttack.Inwait;
		//AnimControl.Play("InDamage");
		//Debug.Log(Dano);
		float FinalDamage =  -Dano;
        Hp -= FinalDamage;
        ShowDamage(FinalDamage);
    }

    public void ShowDamage(float FinalDamage)
    {
        DamageTextPrefab.GetComponent<TextMesh>().text = FinalDamage.ToString();
        DamageTextPrefab.SetActive(true);
        DamageTextPrefab.GetComponent<Animator>().Play("DamageText");        
    }

    public void ReturnToNormal()
    {
		if (IndamageNow)
		{
			TimerDamage += Time.deltaTime;
			if (TimerDamage > 5)
			{
				StatesOfAttackNow = StatesOfAttack.Inwait;
				IndamageNow = false;
				TimerDamage = 0;
			}
		}
	}

    

    public IEnumerator KnockBack(int Dano)
    {		
		AnimControl.SetInteger("ControlAnim", 0);
		IndamageNow = true;
		TimerDamage = 0;
			
		while (TimerKnockBack < 0.02f)
        {
			StatesOfAttackNow = StatesOfAttack.InKnockBack;
            
            TimerKnockBack += Time.deltaTime;
            transform.Translate(Vector3.back * Time.deltaTime * 4);
            yield return null;
        }
        TimerKnockBack = 0;
        controller.Move(-transform.forward);        
        Dodamage(Dano);
		
	}


    public int CalculateAttack(int AttackNow)
    {
        AttackNow += Attack + (Dextry / 2);
        return AttackNow;
    }

}