using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public AudioSource AudioEffect;
    public AudioClip[] DoHit;
    public GameObject HitParticule;
    public GameObject ReferenceHit;
    public GameObject HitNow;
    public GameObject Parent;
    public Animator AnimsControl;
    public enum Attack { First, Second, Third, Final}
    public Attack AttackNow;
    public Player Pm;
    public int DanoOn = 0;
    public bool InDano;
    public int Damage;
    public BaseEnemys Target;
    

    // Start is called before the first frame update
    void Start()
    {
        AttackNow = Attack.First;
        DanoOn = 0;
        InDano = false;
        Target = null;
    }
        
    public void VerifyContinueCombo()
    {
        if (!InDano)
        {
            AudioEffect.clip = DoHit[0];
            AudioEffect.Play();
        }

        if (AttackNow == Attack.First)
        {
            

            if (Pm.ComboControl > 0)
            {

                AnimsControl.SetInteger("FirstComboNow", 1);
                InDano = false;
                
                AttackNow = Attack.Second;
                DanoOn = 1;
                



            }
            else
            {
                Pm.StatesOfAttackNow = Player.StatesOfAttack.Inwait;
                Destroy(HitNow);
                Destroy(Parent);
            }
           
        }

        else if (AttackNow == Attack.Second)
        {

            if (Pm.ComboControl > 1)
            {
                AnimsControl.SetInteger("FirstComboNow", 2);
                InDano = false;
                
                AttackNow = Attack.Third;
                DanoOn = 2;          


            }
            else
            {
                Pm.StatesOfAttackNow = Player.StatesOfAttack.Inwait;
                Destroy(HitNow);
                Destroy(Parent);
            }
            
        }

        else if (AttackNow == Attack.Third)
        {
            Pm.StatesOfAttackNow = Player.StatesOfAttack.Inwait;
            Destroy(HitNow);
            Destroy(Parent);
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        Target = other.GetComponent<BaseEnemys>();
        if (Target != null)
        {
            if (AttackNow == Attack.First)
            {
                if (DanoOn == 0 && !InDano)
                {
                    Target.DoDamage(Damage);
                    Debug.Log("DM = " + Damage);

                    //if (Target != null)
                    //{
                    //    Target.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 1, ForceMode.Impulse);
                    //}

                    HitNow = Instantiate(HitParticule, ReferenceHit.transform.position, Quaternion.identity);
                    AudioEffect.clip = DoHit[1];
                    AudioEffect.Play();


                    InDano = true;

                }
            }

            if (AttackNow == Attack.Second)
            {
                if (DanoOn == 1 && !InDano)
                {
                    Target.DoDamage(Damage + 1);
                    Debug.Log("DM2 = " + (Damage + 1));

                    //if (Target != null)
                    //{
                    //    Target.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 2, ForceMode.Impulse);
                    //}


                    if (HitNow != null)
                    {
                        HitNow.transform.position = ReferenceHit.transform.position;
                        HitNow.GetComponent<ParticleSystem>().Play();
                        AudioEffect.clip = DoHit[1];
                        AudioEffect.Play();
                    }
                    else
                    {
                        HitNow = Instantiate(HitParticule, ReferenceHit.transform.position, Quaternion.identity);
                        AudioEffect.clip = DoHit[1];
                        AudioEffect.Play();
                    }


                    InDano = true;
                }
            }

            if (DanoOn == 2 && !InDano)
            {

                other.GetComponent<BaseEnemys>().DoDamage(Damage + 2);
                Debug.Log("DM3 = " + (Damage + 2));

                //if (Target != null)
                //{
                //    Target.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 20, ForceMode.Impulse);
                //}

                if (HitNow != null)
                {
                    HitNow.transform.position = ReferenceHit.transform.position;
                    HitNow.GetComponent<ParticleSystem>().Play();
                    AudioEffect.clip = DoHit[1];
                    AudioEffect.Play();
                }
                else
                {
                    HitNow = Instantiate(HitParticule, ReferenceHit.transform.position, Quaternion.identity);
                    AudioEffect.clip = DoHit[1];
                    AudioEffect.Play();
                }

                InDano = true;

            }
        }
        
                             
                
    }
}
