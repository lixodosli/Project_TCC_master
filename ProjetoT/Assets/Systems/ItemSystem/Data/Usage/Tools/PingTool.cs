using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Items/Usages/PingTool", fileName = "ToolUsage_PingTool"), System.Serializable]
public class PingTool : Usage
{
    public override void Use(Item item)
    {
        DisplayMessage(item);
    }
}