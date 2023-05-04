using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingCollect : Interactable
{
    public Useable UseableReference;
    public ItemList ItemToCollect;

    public override void DoInteraction()
    {
        if (GameStateManager.Game.State != GameState.World_Free)
            return;

        GameObject item = ItemSpawnPoint.InstItem(ItemToCollect, transform);

        if (item == null)
            return;

        Inventory.Instance.CollectItem(item.GetComponent<Item>());
        UseableReference.SendMessage(0);
        NPCFeitoNasCoxa.Instance.HaveCondition2 = true;
    }
}