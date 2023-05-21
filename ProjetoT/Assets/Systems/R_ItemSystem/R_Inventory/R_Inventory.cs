using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu(menuName = "SO/Inventory")]
public class R_Inventory : ScriptableObject
{
    public int InventorySize;
    public List<R_Item> InventoryItem = new List<R_Item>();
    public int Count => InventoryItem.Count;

    public event Action<R_Inventory> InventoryChanged;

    private void OnEnable()
    {
        InventoryItem = new List<R_Item>();
    }

    public void Add(R_Item item)
    {
        if(Count > 0 && Count % InventorySize == 0)
        {
            FeedbackMessage.ShowFeedback("Este inventário já está cheio.");
            return;
        }

        Debug.Log("Adicionei");
        InventoryItem.Add(item);
        InventoryChanged?.Invoke(this);
    }

    public R_Item GetItem(int index)
    {
        return InventoryItem[index];
    }

    public R_Item GetItem(R_Item item)
    {
        return InventoryItem.Find(i => i.Equals(item));
    }

    public void Remove(R_Item item)
    {
        InventoryItem.Remove(item);
    }
}