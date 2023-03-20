using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class R_Item : R_Interactable
{
    public abstract void UseItem();

    public override void DoInteraction()
    {
        if (GameStateManager.Game.State != GameState.World_Free)
            return;

        Debug.Log("Tentou interagir com o item <" + ItemName + ">.");
        R_Inventory.Instance.CollectItem(this);
    }
}