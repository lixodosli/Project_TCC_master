using UnityEngine;

[System.Serializable]
public class Enxada : R_Item
{
    public override void UseItem()
    {
        Useable_Object closest = ClosestUseable();

        if (closest == null)
            return;

        if (!DateSystem.Instance.CanUpdateHour)
            return;

        UseableManager.Instance.RaiseUseable(new UseableInfo(this, closest));
        GEventManager.Instance.AddGEvent(new GEvent(() => Plages()));
    }

    public void Plages()
    {
        Debug.Log("Some Plages Will Appear in the " + gameObject.name);
    }
}