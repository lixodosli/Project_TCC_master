using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Dialogue System/Dialogue Events")]
public class DialogueEvents : ScriptableObject
{
    public void GiveItem(Item item)
    {
        Debug.Log("Toma esse item ai hihihi");
    }

    public void UpdateQuest(Quest quest)
    {
        Debug.Log("Toma essa quest ai hihi");
    }

    public void RemoveItem(Item item)
    {
        Debug.Log("ME DA ESSA BOSTA");
    }

    public void GoToAnotherConversation(Conversation conversation)
    {
        Debug.Log("Vamo falar sobre outra coisa");
    }
}