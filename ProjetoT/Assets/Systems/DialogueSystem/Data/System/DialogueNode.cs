using System.Collections.Generic;
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

    [SerializeReference] public List<DialogueEffect> Effects = new List<DialogueEffect>();
    [ContextMenu("Add Change Conversation")] public void AddChangeConversation() => Effects.Add(new ChangeConversationEffect());
    [ContextMenu("Add Move NPC")] public void AddMoveNPC() => Effects.Add(new MoveNPCEffect());

    public virtual DialogueNode Dialogue() => this;
}

[System.Serializable]
public class DialogueEffect
{
    public NPC ReferenceNPC;

    public virtual void DoEffect()
    {
    }
}

public class ChangeConversationEffect : DialogueEffect
{
    public NewConversation NewConversation;

    public override void DoEffect()
    {
        ReferenceNPC.ChangeConversation(NewConversation);
    }
}

[System.Serializable]
public class MoveNPCEffect : DialogueEffect
{
    public Transform NewPosition;

    public override void DoEffect()
    {
        ReferenceNPC.transform.position = NewPosition.transform.position;
    }
}