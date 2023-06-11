using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueNode : ScriptableObject
{
    [Header("Configs")]
    [TextArea(1, 1)] public string Name;
    [TextArea] public string Text;
    public float LettersPerSecond;

    [Header("Events")]
    public UnityEvent OnStartDialogue;
    public UnityEvent OnEndDialogue;

    public virtual DialogueNode Dialogue() => this;
}