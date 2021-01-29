using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueActive : MonoBehaviour
{
    public DialogueSystem MyDialogue;
    public int Mydata;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.GetComponent<Player>())
        {
            MyDialogue.ToDestroy = this.gameObject;
            MyDialogue.Index = 0;
            MyDialogue.Sentences = MyDialogue.DataDialogue.DataFile[Mydata].Sentences;
            MyDialogue.Button_Box[1].GetComponent<Image>().sprite = MyDialogue.DataDialogue.DataFile[Mydata].Mybox;
            StartCoroutine(MyDialogue.Type());
        }
    }
}
