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
    public bool groundedPlayer;
    private float gravityValue = -9.81f; 
    public float TimerKnockBack;
    public Animator AnimControl;
    public GameObject DamageTextPrefab;
	public TextMeshProUGUI HpValor;
	

	[Header("STATUS OF JUMP")]

	public InGround Ground;
	public bool IsJumping;
	public bool WasInGround;

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
	public Impact Impact;
	public float ImpactValue;
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

		if (StatesOfAttackNow == StatesOfAttack.InKnockBack)
		{
			AnimControl.SetInteger("ControlAnim", 4);
		}

		
	}

	public void MovePlayer()
	{
		
		if (StatesOfAttackNow == StatesOfAttack.InAttack) return;
		if (StatesOfAttackNow == StatesOfAttack.InKnockBack) return;
		if (Indialogue) return;

		groundedPlayer = Ground.InGroundNow;
		Debug.Log("Anim = " + AnimControl.GetInteger("ControlAnim"));		


		if (Input.GetButtonDown("Jump") && groundedPlayer)
		{
			IsJumping = true;
			AnimControl.SetInteger("ControlAnim", 3);
			playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);			
		}		

		if(IsJumping && !groundedPlayer)
		{
			WasInGround = true;
		}

		else if(WasInGround && IsJumping && groundedPlayer)
		{
			IsJumping = false;
			WasInGround = false;
		}
		

		if (Input.GetButtonDown("Fire2"))
		{
			Dodge();
		}


		
		if (groundedPlayer && playerVelocity.y < 0)
		{
			playerVelocity.y = 0f;
		}

		move = new Vector3(0, 0, Input.GetAxis("Vertical"));

		if (move.z > 0)
		{
			controller.Move(transform.forward * Time.deltaTime * playerSpeed);
			if(!IsJumping && !WasInGround)
			{
				AnimControl.SetInteger("ControlAnim", 1);
			}
			
		}

		else if (move.z < 0)
		{
			controller.Move(transform.forward * Time.deltaTime * -playerSpeed);
			if (!IsJumping && !WasInGround)
			{
				AnimControl.SetInteger("ControlAnim", 1);
			}
		}		

		if (Input.GetAxis("Horizontal") < 0)
		{
			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + Input.GetAxis("Horizontal") * Time.deltaTime * 64, 0);
			if (!IsJumping && !WasInGround && move.z == 0)
			{
				AnimControl.SetInteger("ControlAnim", 0);
			}
		}

		else if (Input.GetAxis("Horizontal") > 0)
		{
			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + Input.GetAxis("Horizontal") * Time.deltaTime * 64, 0);
			if (!IsJumping && !WasInGround && move.z == 0)
			{
				AnimControl.SetInteger("ControlAnim", 0);
			}
		}

		if (!IsJumping && !WasInGround && move.z == 0 )
		{
			AnimControl.SetInteger("ControlAnim", 0);
		}

		// Changes the height position of the player..


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
		
		//AnimControl.Play("InDamage");
		//Debug.Log(Dano);
		float FinalDamage =  -Dano;
        Hp -= FinalDamage;
        ShowDamage(FinalDamage);
		//StatesOfAttackNow = StatesOfAttack.Inwait;
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

    

    public void KnockBack(int Dano)
    {
		StatesOfAttackNow = StatesOfAttack.InKnockBack;		
		IndamageNow = true;
		TimerDamage = 0;
		Impact.AddImpact(-transform.forward, ImpactValue);		
		Dodamage(Dano);
		
	}


	public void Dodge()
	{
		StatesOfAttackNow = StatesOfAttack.InKnockBack;
		AnimControl.SetInteger("ControlAnim", 0);
		Impact.AddImpact(-transform.forward, ImpactValue);
		StatesOfAttackNow = StatesOfAttack.Inwait;
	}



	public int CalculateAttack(int AttackNow)
    {
        AttackNow += Attack + (Dextry / 2);
        return AttackNow;
    }

}