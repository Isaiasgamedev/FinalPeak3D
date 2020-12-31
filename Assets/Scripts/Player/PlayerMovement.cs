using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    CharacterController controller;
    public Vector3 move;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;

    [Header("STATUS OF PLAYER")]
    
    
    public int Hp;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public int Attack;
    public int Defense;
    public int Agility;
    public int Dextry;


    [Header("STATES OF ATTACK")]
    public StatesOfAttack StatesOfAttackNow;
    public enum StatesOfAttack { Inwait, InAttack}    
    public int ComboControl;
    public GameObject WeaponInUseNow;
    public GameObject MelleAttack;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();      
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);       

        if (move != Vector3.zero)
        {
            transform.forward = Vector3.Lerp(transform.forward, move, Time.deltaTime * 8);
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (Input.GetButtonDown("Fire1"))
        {
            if(ManagerItens.Instance.WeaponDataNow.DataNow[0].Level > 0)
            {
                if (StatesOfAttackNow == StatesOfAttack.Inwait)
                {
                    ComboControl = 0;
                    GameObject WeaponNow = Instantiate(MelleAttack, WeaponInUseNow.transform.position, Quaternion.identity, WeaponInUseNow.transform);
                    WeaponNow.transform.localEulerAngles = transform.forward;
                    WeaponNow.GetComponentInChildren<Sword>().Pm = this;
                    WeaponNow.GetComponentInChildren<Sword>().Damage += CalculateAttack(0);
                    StatesOfAttackNow = StatesOfAttack.InAttack;

                }
                else
                {
                    ComboControl++;
                }
            }
            
            
        }
    }

    public int CalculateAttack(int AttackNow)
    {
        AttackNow += Attack + (Dextry / 2);

        return AttackNow;
    }

}