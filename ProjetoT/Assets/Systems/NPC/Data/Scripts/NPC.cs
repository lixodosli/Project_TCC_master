using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public List<Narratives> Narratives;
    public int CurrentNarrativeIndex;

    public override void DoInteraction()
    {
        DialogueSystem.Instance.StartConversation(this);
    }

    public virtual void NextNarrative()
    {
        CurrentNarrativeIndex++;
    }

    public virtual void ChangeNarrative(int index)
    {
        CurrentNarrativeIndex = index;
    }

    public virtual void ChangeNarrative(NewConversation conversation)
    {
        bool found = false;
        int foundedNarrative = 0;

        for (int i = 0; i < Narratives.Count; i++)
        {
            found = Narratives[i].NConversations.Find(c => c.ConversationContext == conversation.ConversationContext) != null;

            if (found)
            {
                foundedNarrative = i;
                break;
            }
        }

        if (!found)
            return;

        CurrentNarrativeIndex = foundedNarrative;
    }
}

[System.Serializable]
public class Narratives
{
    public List<Conversation> Conversations;
    public List<NewConversation> NConversations;
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