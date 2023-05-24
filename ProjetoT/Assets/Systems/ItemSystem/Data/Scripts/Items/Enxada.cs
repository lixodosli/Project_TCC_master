using UnityEngine;
using System;

[System.Serializable]
public class Enxada : Item
{
    public override void UseItem()
    {
        base.UseItem();
    }

    public void Plages()
    {
        Debug.Log("Some Plages Will Appear in the " + gameObject.name);
    }
}