using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueNode : ScriptableObject
{
    [Header("Configs")]
    [TextArea(1, 1)] public string Name;
    [TextArea] public string Text;
    public float LettersPerSecond;

    [Header("Events")]
    [SerializeReference] public List<DialogueEffect> Effects = new List<DialogueEffect>();
    public bool DoEffectsOnStart;
    public UnityEvent OnStartDialogue;
    public UnityEvent OnEndDialogue;

    [ContextMenu("Add Change Conversation")] public void AddChangeConversation() => Effects.Add(new ChangeConversationEffect());
    [ContextMenu("Add Move NPC")] public void AddMoveNPC() => Effects.Add(new MoveNPCEffect());
    [ContextMenu("Add Add Item")] public void AddAddItem() => Effects.Add(new AddItemEffect());
    [ContextMenu("Add Remove Item")] public void AddRemoveItem() => Effects.Add(new RemoveItemEffect());
    [ContextMenu("Add Start Quest")] public void AddStartQuest() => Effects.Add(new StartQuestEffect());
    [ContextMenu("Add Move Player")] public void AddMovePlayer() => Effects.Add(new MovePlayerEffect());
    [ContextMenu("Add Wait Time Player")] public void AddWaitTime() => Effects.Add(new WaitTimeEffect());

    public void DoEffects() => Effects.ForEach(e => e.DoEffect());

    public virtual DialogueNode NextDialogue() { return null; }
}

[System.Serializable]
public class DialogueEffect
{
    public NPCConfig ReferenceNPC;

    public virtual void DoEffect()
    {
    }
}

[System.Serializable]
public class ChangeConversationEffect : DialogueEffect
{
    public NewConversation NewConversation;

    public override void DoEffect()
    {
        ReferenceNPC.ChangeConversation(NewConversation);
    }
}

[System.Serializable]
public class MoveNPCEffect : DialogueEffect
{
    public Transform NewPosition;

    public override void DoEffect()
    {
        //ReferenceNPC.transform.position = NewPosition.transform.position;
    }
}

[System.Serializable]
public class AddItemEffect : DialogueEffect
{
    public List<GameObject> ItemToGive = new List<GameObject>();

    public override void DoEffect()
    {
        foreach (var item in ItemToGive)
        {
            GameObject c = MonoBehaviour.Instantiate(item, Inventory.Instance.transform);
            c.GetComponent<Item>().SetID();
            Inventory.Instance.CollectItem(c.GetComponent<Item>());
        }
    }
}

[System.Serializable]
public class RemoveItemEffect : DialogueEffect
{
    public List<GameObject> ItemsToRemove = new List<GameObject>();

    public override void DoEffect()
    {
        bool[] haveItem = new bool[ItemsToRemove.Count];
        List<Item> checkedItems = new List<Item>(); // New list to store checked items

        for (int itemCount = 0; itemCount < ItemsToRemove.Count; itemCount++)
        {
            Item checkageItem = ItemsToRemove[itemCount].GetComponent<Item>();

            if (checkageItem == null)
                continue;

            for (int inventoryCount = 0; inventoryCount < Inventory.Instance.Items.Length; inventoryCount++)
            {
                Item inventoryItem = Inventory.Instance.Items[inventoryCount];

                if (inventoryItem == null)
                    continue;

                // Check if the item has already been checked
                if (checkedItems.Contains(inventoryItem))
                {
                    continue; // Skip to the next iteration if already checked
                }

                if (inventoryItem.ItemName == checkageItem.ItemName)
                {
                    haveItem[itemCount] = true;
                    checkedItems.Add(inventoryItem); // Add the item to the checked list
                    Inventory.Instance.ConsumeItem(inventoryItem); // Consume the item
                    break; // Exit the inner loop since the item has been found
                }
            }

            if (!haveItem[itemCount])
            {
                Debug.Log("N tem os itens necessarios");
                return;
            }
        }
    }
}

[System.Serializable]
public class StartQuestEffect : DialogueEffect
{
    public Quest QuestToStart;

    public override void DoEffect()
    {
        PlayerQuests.Instance.AddQuest(QuestToStart);
    }
}

[System.Serializable]
public class MovePlayerEffect : DialogueEffect
{
    public Transform NewPosition;

    public override void DoEffect()
    {
        PlayerMovement.Player.transform.position = NewPosition.transform.position;
    }
}

[System.Serializable]
public class WaitTimeEffect : DialogueEffect
{
    public string TimerName;
    public int TimeToWaitInHours;

    public override void DoEffect()
    {
        TimeCounterAlongTime timer = new TimeCounterAlongTime(TimerName, TimeToWaitInHours);

        TimeManager.Instance.OnTotalHourChange += timer.UpdateCounter;
        TimeManager.Instance.Timers.Add(timer);
    }
}

// Contabilizar contribuicao