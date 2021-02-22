using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueActive : MonoBehaviour
{
    public DialogueSystem MyDialogue;
	public Animator Shieldanim;
	public Animator FileAnim;
	public bool TextOn;
    public int Mydata;
	

    private void OnTriggerEnter(Collider other)
    {
		//Debug.Log(other.name);
		if (!TextOn)
		{
			if (other.GetComponent<Player>())
			{
				MyDialogue.Avatar.GetComponent<Image>().sprite = MyDialogue.DataDialogue.DataFile[Mydata].Avatar;
				MyDialogue.NameDisplay.text = MyDialogue.DataDialogue.DataFile[Mydata].Name;
				MyDialogue.ToDestroy = this.gameObject;
				MyDialogue.ShieldAnim = Shieldanim;
				MyDialogue.FileAnim = FileAnim;
				MyDialogue.Index = 0;
				MyDialogue.Sentences = MyDialogue.DataDialogue.DataFile[Mydata].Sentences;
				MyDialogue.Button_Box[1].GetComponent<Image>().sprite = MyDialogue.DataDialogue.DataFile[Mydata].Mybox;
				StartCoroutine(MyDialogue.Type());
				TextOn = true;

			}
		}
        
    }

	
}
