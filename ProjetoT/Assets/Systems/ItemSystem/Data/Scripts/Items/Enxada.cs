using UnityEngine;

[System.Serializable]
public class Enxada : Item
{
    public override void UseItem()
    {
        Useable_Set closest = ClosestUseable();

        if (closest == null)
            return;

        if (!DateSystem.Instance.CanUpdateHour)
            return;

        closest.UseUseable(this);
        GEventManager.Instance.AddGEvent(new GEvent(() => Plages()));
    }

    public void Plages()
    {
        Debug.Log("Some Plages Will Appear in the " + gameObject.name);
    }
}