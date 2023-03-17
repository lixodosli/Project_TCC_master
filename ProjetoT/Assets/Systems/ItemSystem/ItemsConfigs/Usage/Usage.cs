using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usage : MonoBehaviour
{
    public string UseText;
    [TextArea(3, 4)] public string UseDescription;
    [TextArea(0, 1)] public string OnUseDebugMessage;

    public virtual void Use(Item item)
    {
        return;
    }

    public void DisplayMessage(Item item)
    {
        Debug.Log(item.ItemName + ": " + OnUseDebugMessage);
    }

    public void DisplayMessage()
    {
        Debug.Log(OnUseDebugMessage);
    }
}