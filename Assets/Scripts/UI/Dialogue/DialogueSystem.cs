using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI TextDisplay;
    public string[] Sentences;
    public int Index;
    public WaitForSeconds Seconds = new WaitForSeconds(0.02f);
    public GameObject[] Button_Box;
    public Animator textanim;
    public DataDialoguesSystem DataDialogue;
    public Player PlayerControl;
    public GameObject ToDestroy;


    public void Start()
    {
        Sentences = DataDialogue.DataPlayer[0].Sentences;
        StartCoroutine(Type());       
    }

    public void Update()
    {
        if(TextDisplay.text == Sentences[Index])
        {
            Button_Box[0].SetActive(true);
            Button_Box[0].GetComponent<Button>().Select();
            textanim.Play("Idle");
        }
    }

    public IEnumerator Type()
    {
        PlayerControl.Indialogue = true;
        Button_Box[1].SetActive(true);
        //Button_Box[1].GetComponent<Image>().sprite = DataDialogue.DataPlayer[0].Mybox;
        foreach (char letter in Sentences[Index].ToCharArray())
        {
            TextDisplay.text += letter;
            yield return Seconds;
        }
        
    }

    public void NextSentence()
    {
        textanim.Play("Change");
        Button_Box[0].SetActive(false);
        if (Index < Sentences.Length -1)
        {
            Index++;
            TextDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            TextDisplay.text = "";
            Button_Box[0].SetActive(false);
            Button_Box[1].SetActive(false);
            PlayerControl.Indialogue = false;
            DestoyObjects();
        }
    }

    public void DestoyObjects()
    {
        Destroy(ToDestroy);
    }
}
