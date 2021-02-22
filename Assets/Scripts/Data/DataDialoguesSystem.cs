using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

[Serializable]
public class DataDialoguePlayer
{
	public Sprite Avatar;
    public Sprite Mybox;
    public string Name;
    public string[] Sentences;
}

[Serializable]
public class DataDialogueFILE
{
	public Sprite Avatar;
	public Sprite Mybox;
    public string Name;
    public string[] Sentences;
}

[Serializable]
public class DataDialogueReuse
{
	public Sprite Avatar;
	public string Name;
    public string[] Sentences;
}

public class DataDialoguesSystem : ScriptableObject
{
    public DataDialoguePlayer[] DataPlayer;
    public DataDialogueFILE[] DataFile;
}
