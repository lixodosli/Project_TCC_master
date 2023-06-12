using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingCollect : Interactable
{
    public Useable UseableReference;
    public List<GameObject> ItemToCollect = new List<GameObject>();

    public override void DoInteraction()
    {
        if (GameStateManager.Game.State != GameState.World_Free)
            return;

        foreach (GameObject item in ItemToCollect)
        {
            GameObject c = Instantiate(item, Inventory.Instance.transform);
            c.GetComponent<Item>().SetID();
            Inventory.Instance.CollectItem(c.GetComponent<Item>());
        }

        UseableReference.SendMessage(0);
    }
}