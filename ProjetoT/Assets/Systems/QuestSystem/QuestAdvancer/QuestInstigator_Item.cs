using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Quests/Instigator_Item")]
public class QuestInstigator_Item : QuestInstigator
{
    public Item[] ItemsNeeded;

    public override bool CanUpdateQuestProgress()
    {
        return Inventory.Instance.VerifyInventory(ItemsNeeded);
    }

    public override void UpdateQuestProgress(Quest quest)
    {
        if (!CanUpdateQuestProgress())
            return;

        PlayerQuests.UpdateProgress(quest);
    }
}