using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Useable : MonoBehaviour
{
    public R_Item[] Interact;

    public virtual bool CanBeUsed(R_Item item)
    {
        bool canUse = false;

        for (int i = 0; i < Interact.Length; i++)
        {
            if(Interact[i].ItemName == item.ItemName)
            {
                canUse = true;
                break;
            }
        }

        return canUse;
    }

    public abstract void OnUsed(R_Item item);
}