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
        {
            FeedbackMessage.ShowFeedback("Voc� n�o pode utilizar este item agora.");
            return;
        }

        if (closest.TryUse(this))
        {
            Inventory.Instance.ConsumeItem(this);
            gameObject.SetActive(false);
            closest.UseUseable(this);
            NPCFeitoNasCoxa.Instance.HaveCondition1 = true;
        }
    }
}