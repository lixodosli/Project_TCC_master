using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class R_Item : R_Interactable
{
    private void Start()
    {
        if(ItemID == null)
        {
            Debug.Log("O item <" + gameObject.name + "> Está sem ID, portanto precisa de um ID.");
        }
    }

    public abstract void UseItem();

    public override void DoInteraction()
    {
        if (GameStateManager.Game.State != GameState.World_Free)
            return;

        R_Inventory.Instance.CollectItem(this);
    }

    protected virtual Useable_Object ClosestUseable()
    {
        Collider[] closeObjects = Physics.OverlapSphere(transform.position, 1.5f);
        List<Useable_Object> useableObjects = new List<Useable_Object>();

        bool haveUseableItem = false;

        for (int i = 0; i < closeObjects.Length; i++)
        {
            if (closeObjects[i].GetComponent<Useable_Object>() != null)
            {
                haveUseableItem = true;
                useableObjects.Add(closeObjects[i].GetComponent<Useable_Object>());
            }
        }

        if (!haveUseableItem)
            return null;

        int closestIndex = 0;

        for (int i = 0; i < useableObjects.Count; i++)
        {
            if (Vector3.Distance(transform.position, useableObjects[i].transform.position) < Vector3.Distance(transform.position, useableObjects[closestIndex].transform.position))
            {
                closestIndex = i;
            }
        }

        if (!useableObjects[closestIndex].CurrentState().CanBeUsed(this))
            return null;

        return useableObjects[closestIndex];
    }
}