using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Items/Item", fileName = "Item_")]
public class Item : ScriptableObject
{
    [Header("Basic Item Info")]
    public string ItemName;
    public string ItemDescription;
    public Sprite ItemIcon;

    [Header("Item Effects")]
    public Usage Usage;
    public InteractableItem GameItem;

    [Header("Item ID")]
    public ItemID ItemID;

    public virtual bool CanUse()
    {
        return true;
    }

    public void UseItem()
    {
        Usage.Use(this);
    }

    public void GenerateID()
    {
        ItemID = new ItemID();
    }
}

public class ItemID
{
    public string ID;

    public ItemID()
    {
        ID = System.Guid.NewGuid().ToString();
    }
}