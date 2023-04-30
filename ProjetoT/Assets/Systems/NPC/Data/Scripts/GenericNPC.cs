using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericNPC : Interactable
{
    public List<Conversation> m_Conversations;
    [SerializeField] private int m_CurrentConversationIndex;

    public override void DoInteraction()
    {
        DialogueSystem.Instance.StartConversation(m_Conversations[m_CurrentConversationIndex].Name);
    }
}