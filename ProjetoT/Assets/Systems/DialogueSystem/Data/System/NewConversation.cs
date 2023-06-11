using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Scriptable Objects/Dialogue System/NewConversation")]
public class NewConversation : ScriptableObject
{
    [Header("Configs")]
    [TextArea] public string ConversationContext;

    [Header("Dialogue")]
    [SerializeReference] public DialogueNode FirstDialogue;
}