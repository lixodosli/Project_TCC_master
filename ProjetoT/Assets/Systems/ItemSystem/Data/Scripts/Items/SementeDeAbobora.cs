using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SementeDeAbobora : R_Item
{
    public override void UseItem()
    {
        Useable_Object closest = ClosestUseable();

        if (closest == null)
            return;

        if (!DateSystem.Instance.CanUpdateHour)
            return;

        R_Inventory.Instance.ConsumeItem(this);
        gameObject.SetActive(false);
        UseableManager.Instance.RaiseUseable(new UseableInfo(this, closest));
    }
}