using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Scriptable Objects/Dialogue System/Conversation")]
public class Conversation : ScriptableObject
{
    public Quest RequiredQuest;
    public string Name;
    public Quest GiveQuest;
    public List<Dialogue> Dialogues = new List<Dialogue>();
}

[System.Serializable]
public class Dialogue
{
    public string Name;
    [TextArea] public string Text;
    public int[] Options;
    public float LettersPerSecond;
    public UnityEvent OnEndDialogue;
}