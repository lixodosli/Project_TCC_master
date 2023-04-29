using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item : Interactable
{
    private void Awake()
    {
        UpdateParent();
    }

    private void Start()
    {
        if(ItemID == null)
        {
            Debug.Log("O item <" + gameObject.name + "> Está sem ID, portanto precisa de um ID.");
        }
    }

    private void OnTransformParentChanged()
    {
        UpdateParent();
    }

    public void SetItemNameAndId(string name, string id)
    {
        m_ItemName = name;
        m_ItemID = id;
        m_ToolTip = GetComponent<ToolTip>();
    }

    public virtual void UseItem()
    {
        Useable_Set closest = ClosestUseable();

        if (closest == null)
            return;

        closest.UseUseable(this);
    }

    public override void DoInteraction()
    {
        if (GameStateManager.Game.State != GameState.World_Free)
            return;

        Inventory.Instance.CollectItem(this);
    }

    protected virtual Useable_Set ClosestUseable()
    {
        return NearbyUseableSets.ClosestUseableSet;
    }

    public void UpdateParent()
    {
    }

    protected virtual void RestoreItem()
    {
    }
}