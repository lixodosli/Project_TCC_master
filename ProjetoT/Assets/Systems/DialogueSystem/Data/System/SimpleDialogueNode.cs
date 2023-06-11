using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Simple Node", menuName = "Scriptable Objects/Dialogue System/Dialogue/Simple Node")]
public class SimpleDialogueNode : DialogueNode
{    
    [Header("Structure")]
    public DialogueNode NextDialog;

    public override DialogueNode NextDialogue() => NextDialog;
}