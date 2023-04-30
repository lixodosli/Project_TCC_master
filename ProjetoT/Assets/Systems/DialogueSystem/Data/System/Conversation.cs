using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Scriptable Objects/Dialogue System/Conversation")]
public class Conversation : ScriptableObject
{
    public string Name;
    public List<Dialogue> Dialogues = new List<Dialogue>();
}

[System.Serializable]
public class Dialogue
{
    public string Name;
    [TextArea] public string Text;
    public int[] Options;
    public float LettersPerSecond;
}