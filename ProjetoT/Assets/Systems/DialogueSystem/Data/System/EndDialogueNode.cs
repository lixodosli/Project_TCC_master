using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDialogueNode : DialogueNode
{
    [SerializeReference] public List<EndDialogueEffect> Effects = new List<EndDialogueEffect>();

    [ContextMenu("Add Change Conversation")] public void ChangeConversation() => Effects.Add(new ChangeConversationDialogueEffect());
    [ContextMenu("Add Move NPC")] public void MoveNPC() => Effects.Add(new MoveNPCDialogueEffect());
}

[System.Serializable]
public class EndDialogueEffect
{
    public NPC ReferenceNPC;

    public virtual void DoEffect() { }
}

public class ChangeConversationDialogueEffect : EndDialogueEffect
{
    public NewConversation NextConversation;

    public override void DoEffect()
    {
        ReferenceNPC.ChangeNarrative(NextConversation);
    }
}

public class MoveNPCDialogueEffect : EndDialogueEffect 
{
    public Transform NewSpot;

    public override void DoEffect()
    {
        ReferenceNPC.transform.position = NewSpot.position;
    }
}