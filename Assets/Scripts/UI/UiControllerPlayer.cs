using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiControllerPlayer : MonoBehaviour
{
	public Player PlayerControl;
	public Image Hp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Hp.fillAmount = PlayerControl.Hp / 100;
	}
}
