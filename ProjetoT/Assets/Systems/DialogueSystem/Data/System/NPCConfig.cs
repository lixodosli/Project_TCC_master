using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC_", menuName = "Scriptable Objects/NPC")]
public class NPCConfig : ScriptableObject
{
    public NewConversation FirstConversation;
    public NewConversation CurrentConversation { get; private set; } = null;

    private void OnEnable()
    {
        CurrentConversation = null;
    }

    public void ChangeConversation(NewConversation newConversation) => CurrentConversation = newConversation;
}