using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Condition Node", menuName = "Scriptable Objects/Dialogue System/Dialogue/Condition Node")]
public class ConditionDialogueNode : DialogueNode
{
    [SerializeReference] public List<DialogueCondition> Conditions = new List<DialogueCondition>();

    [ContextMenu("Item Condition")] public void AddItemCondition() => Conditions.Add(new ItemDialogueCondition());
    [ContextMenu("Quest Condition")] public void AddQuestCondition() => Conditions.Add(new QuestDialogueCondition());
    [ContextMenu("Day Time Condition")] public void AddDayTimeCondition() => Conditions.Add(new DayTimeDialogueCondition());
    [ContextMenu("Wait Time Condition")] public void AddWaitTimeCondition() => Conditions.Add(new WaitTimeDialogueCondition());
    [ContextMenu("Donation Condition")] public void AddDonationCondition() => Conditions.Add(new DonationLevelCondition());

    [Header("Dialogues")]
    public DialogueNode IfTrue;
    public DialogueNode IfFalse;

    public override DialogueNode NextDialogue()
    {
        foreach (DialogueCondition condition in Conditions)
        {
            if (!condition.HaveCondition())
                return IfFalse;
        }

        return IfTrue;
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
        bool haveItem = false;

        foreach (Item item in itemsInInventory)
        {
            if (item != null)
            {
                haveItem = true;
                break;
            }
        }

        if (haveItem)
        {
            foreach (Item item in Inventory.Instance.Items)
            {
                if(item != null && item.ItemName == ItemToCheck.ItemName)
                    checkageCount++;
            }
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
public class DayTimeDialogueCondition : DialogueCondition
{
    [Header("Time Configs")]
    public int Checkage;
    public bool TotalHour;

    public override bool HaveCondition() => TotalHour ? Checkage >= TimeManager.TotalHours : TimeManager.CurrentHour >= Checkage;
}

[System.Serializable]
public class WaitTimeDialogueCondition : DialogueCondition
{
    [Header("Time Configs")]
    public string TimerName;

    public override bool HaveCondition()
    {
        TimeCounterAlongTime timer = TimeManager.Instance.Timers.Find(a => a.TimerName == TimerName);

        if (timer == null || !timer.Achieve)
            return false;

        TimeManager.Instance.OnTotalHourChange -= timer.UpdateCounter;
        TimeManager.Instance.Timers.Remove(timer);
        return true;
    }
}

[System.Serializable]
public class DonationLevelCondition : DialogueCondition
{
    public int DonationLevel;
    public bool DonationInPoints;

    public override bool HaveCondition()
    {
        return DonationInPoints ? DonationsManager.Instance.Structure.CurrentDonationsPoints >= DonationLevel : DonationsManager.Instance.Structure.CurrentMilestoneIndex >= DonationLevel;
    }
}