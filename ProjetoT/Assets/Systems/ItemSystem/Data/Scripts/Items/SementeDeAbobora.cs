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

        Inventory.Instance.ConsumeItem(this);
        gameObject.SetActive(false);
        closest.UseUseable(this);
        NPCFeitoNasCoxa.Instance.HaveCondition1 = true;
        //UseableManager.Instance.RaiseUseable(new UseableInfo(this, closest));
    }
}