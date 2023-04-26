using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SementeDeAbobora : Item
{
    public override void UseItem()
    {
        Useable_Set closest = ClosestUseable();

        if (closest == null)
            return;

        if (!DateSystem.Instance.CanUpdateHour)
            return;

        Inventory.Instance.ConsumeItem(this);
        gameObject.SetActive(false);
        closest.UseUseable(this);
        //UseableManager.Instance.RaiseUseable(new UseableInfo(this, closest));
    }
}