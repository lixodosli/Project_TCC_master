using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public NewConversation CurrentConversation { get; private set; }

    public override void DoInteraction()
    {
        NewDialogueSystem.Instance.StartConversation(CurrentConversation);
    }

    public void ChangeConversation(NewConversation newConversation) => CurrentConversation = newConversation;

    //public List<Narratives> Narratives;
    //public int CurrentNarrativeIndex;

    //public override void DoInteraction()
    //{
    //    DialogueSystem.Instance.StartConversation(this);
    //}

    //public virtual void NextNarrative()
    //{
    //    CurrentNarrativeIndex++;
    //}

    //public virtual void ChangeNarrative(int index)
    //{
    //    CurrentNarrativeIndex = index;
    //}
}

[System.Serializable]
public class Narratives
{
    public List<Conversation> Conversations;
    public int CurrentConversationIndex;

    public void NextConversation()
    {
        CurrentConversationIndex++;
    }

    public void ChangeConversation(int index)
    {
        CurrentConversationIndex = index;
    }
}