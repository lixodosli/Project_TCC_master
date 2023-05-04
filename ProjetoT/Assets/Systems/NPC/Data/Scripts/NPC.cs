using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public List<Conversation> m_Conversations;
    [SerializeField] protected int m_CurrentConversationIndex;

    public override void DoInteraction()
    {
        DialogueSystem.Instance.StartConversation(m_Conversations[m_CurrentConversationIndex].Name);
    }

    public virtual void ChangeConversation(int index)
    {
        m_CurrentConversationIndex = index;
    }
}