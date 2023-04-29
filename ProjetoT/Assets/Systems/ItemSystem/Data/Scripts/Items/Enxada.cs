using UnityEngine;
using System;

[System.Serializable]
public class Enxada : Item
{
    public override void UseItem()
    {
        TimeManager.Instance.AddEvent(new GEvent(() => Plages()));
        Useable_Set closest = ClosestUseable();

        if (closest == null)
            return;

        closest.UseUseable(this);
        //GEventManager.Instance.AddGEvent(new GEvent(() => Plages()));
    }

    public void Plages()
    {
        Debug.Log("Some Plages Will Appear in the " + gameObject.name);
    }
}