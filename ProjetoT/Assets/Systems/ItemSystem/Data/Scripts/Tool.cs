using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Items/Item/Tool", fileName = "Tool_")]
public class Tool : Item
{
    public override bool CanUse()
    {
        return true;
    }
}