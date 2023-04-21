using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Balde : Item
{
    public override void UseItem()
    {
        Useable_Set closest = ClosestUseable();

        if (closest == null)
            return;

        if (!DateSystem.Instance.CanUpdateHour)
            return;

        closest.UseUseable(this);
        //UseableManager.Instance.RaiseUseable(new UseableInfo(this, closest));
    }
}