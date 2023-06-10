using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Simple Node", menuName = "Scriptable Objects/Dialogue System/Dialogue/Simple Node")]
public class SimpleDialogueNode : DialogueNode
{
    [Header("Configs")]
    [TextArea(1, 1)] public string Name;
    [TextArea] public string Text;
    public float LettersPerSecond;
    
    [Header("Structure")]
    public DialogueNode NextDialog;

    [Header("Events")]
    public UnityEvent OnStartDialogue;
    public UnityEvent OnEndDialogue;

    public override void Execute()
    {
        
    }
}