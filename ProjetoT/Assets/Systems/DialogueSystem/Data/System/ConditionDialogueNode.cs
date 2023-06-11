using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Condition Node", menuName = "Scriptable Objects/Dialogue System/Dialogue/Condition Node")]
public class ConditionDialogueNode : DialogueNode
{
    [SerializeReference] public List<DialogueCondition> Conditions = new List<DialogueCondition>();

    [ContextMenu("Item Condition")] public void AddItemCondition() => Conditions.Add(new ItemDialogueCondition());
    [ContextMenu("Quest Condition")] public void AddQuestCondition() => Conditions.Add(new QuestDialogueCondition());
    [ContextMenu("Time Condition")] public void AddTimeCondition() => Conditions.Add(new TimeDialogueCondition());

    [Header("Dialogues")]
    public DialogueNode IfTrue;
    public DialogueNode IfFalse;

    public override DialogueNode NextDialogue()
    {
        foreach (DialogueCondition condition in Conditions)
        {
            if (!condition.HaveCondition())
                return IfFalse.NextDialogue();
        }

        return IfTrue.NextDialogue();
    }
}

[System.Serializable]
public class DialogueCondition
{
    public virtual bool HaveCondition() => true;
}

[System.Serializable]
public class ItemDialogueCondition : DialogueCondition
{
    [Header("Item Configs")]
    public Item ItemToCheck;
    public int ItemQuantity;

    public override bool HaveCondition()
    {
        Item[] itemsInInventory = Inventory.Instance.Items;
        int checkageCount = 0;

        foreach (Item item in itemsInInventory)
        {
            if(item.ItemName == ItemToCheck.ItemName)
                checkageCount++;
        }

        if (checkageCount >= ItemQuantity)
            return true;
        else return false;
    }
}

[System.Serializable]
public class QuestDialogueCondition : DialogueCondition
{
    [Header("Quest Configs")]
    public Quest RequiredQuest;

    public override bool HaveCondition() => PlayerQuests.Instance.IsCompleted(RequiredQuest);
}

[System.Serializable]
public class TimeDialogueCondition : DialogueCondition
{
    [Header("Time Configs")]
    public int Checkage;
    public bool TotalHour;

    public override bool HaveCondition() => TotalHour ? Checkage >= TimeManager.TotalHours : TimeManager.CurrentHour >= Checkage;
}