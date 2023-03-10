using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Usage : ScriptableObject
{
    [SerializeField, TextArea(3, 4)] protected string m_UseDescription;
    [SerializeField, TextArea(0, 1)] protected string m_OnUseDebugMessage;

    public abstract void Use(Item item);

    protected void DisplayMessage(Item item)
    {
        Debug.Log(item.ItemName + ": " + m_OnUseDebugMessage);
    }
}