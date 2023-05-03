using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Dialogue System/Dialogue Events")]
public class DialogueEvents : ScriptableObject
{
    public void GiveItem(GameObject item)
    {
        for (int i = 0; i < 3; i++)
        {
            //GameObject c = ItemSpawnPoint.InstItem(ItemList.SementeDeAbóbora, Inventory.Instance.transform);

            GameObject c = Instantiate(item, TimeManager.Instance.transform);
            c.GetComponent<Item>().SetID();
            Inventory.Instance.CollectItem(c.GetComponent<Item>());
        }
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

    public void ChangeConversationIndex(int index)
    {
        NPCFeitoNasCoxa.Instance.ChangeConversation(index);
    }

    public void VaiPraLonge()
    {
        NPCFeitoNasCoxa.Instance.VaiPraOndeJudasPerdeuAsBotas();
    }
}