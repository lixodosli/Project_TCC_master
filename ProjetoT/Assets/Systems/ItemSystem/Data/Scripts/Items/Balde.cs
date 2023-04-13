using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Balde : R_Item
{
    public override void UseItem()
    {
        Useable_Object closest = ClosestUseable();

        if (closest == null)
            return;

        if (!DateSystem.Instance.CanUpdateHour)
            return;

        UseableManager.Instance.RaiseUseable(new UseableInfo(this, closest));
    }
}