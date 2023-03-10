using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Items/Inventory Channel", fileName = "Inventory Channel")]
public class InventoryChannel : ScriptableObject
{
    public delegate void InventoryCallback(Item item);
    public event InventoryCallback OnUseItem;
    public event InventoryCallback OnAddItem;
    public event InventoryCallback OnRemoveItem;

    public delegate void InventoryCallback_Display(bool show);
    public event InventoryCallback_Display OnInventoryDisplayShow;

    public delegate void InventoryCallback_Selection(int index);
    public event InventoryCallback_Selection OnInventorySelect;

    public void RaiseUseItem(Item item) => OnUseItem?.Invoke(item);

    public void RaiseAddItem(Item item) => OnAddItem?.Invoke(item);

    public void RaiseRemoveItem(Item item) => OnRemoveItem?.Invoke(item);

    public void RaiseInventoryDisplayShow(bool show) => OnInventoryDisplayShow?.Invoke(show);

    public void RaiseInventorySelection(int index) => OnInventorySelect?.Invoke(index);
}