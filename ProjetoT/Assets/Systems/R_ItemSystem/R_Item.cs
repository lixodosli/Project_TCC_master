using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item")]
public class R_Item : ScriptableObject
{
    public string ItemName;
    public string ItemDescription;
    [PreviewSprite] public Sprite ItemIcon;
    public GameObject ItemModel;
    [SerializeReference] public R_ItemType Type;

    [ContextMenu("Consumable")] public void AddConsumableType() => Type = new R_ItemType_Consumable();
    [ContextMenu("Tool")] public void AddToolType() => Type = new R_ItemType_Tool();
    [ContextMenu("Reset")] public void ResetType() => Type = null;

    public void Use() => Type.Use();
}

[System.Serializable]
public class R_ItemType
{
    public virtual void Use()
    {

    }

    public virtual void Drop()
    {

    }
}

[System.Serializable]
public class R_ItemType_Consumable : R_ItemType
{
    [ReadOnly] public string Type = "Consumable";
    public int ItemMaxUsage;
    private int _CurrentUseage = 0;
    public R_Item TransformsTo;

    public override void Use()
    {
        _CurrentUseage++;
        
        if(_CurrentUseage >= ItemMaxUsage)
        {
            //Do Something;
        }
    }

    public void DoTransform()
    {
        if (TransformsTo == null)
            return;

    }
}

[System.Serializable]
public class R_ItemType_Tool : R_ItemType
{
    [ReadOnly] public string Type = "Tool";

    public override void Use()
    {
        base.Use();
    }
}