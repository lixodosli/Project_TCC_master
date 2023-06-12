using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public NPCConfig Config;

    private void Start()
    {
        if (Config.CurrentConversation == null)
            ChangeConversation(Config.FirstConversation);
    }

    public override void DoInteraction()
    {
        NewDialogueSystem.Instance.StartConversation(Config.CurrentConversation);
    }

    public void ChangeConversation(NewConversation newConversation) => Config.ChangeConversation(newConversation);
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