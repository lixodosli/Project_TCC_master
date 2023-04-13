using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class R_Item : R_Interactable
{
    private ItemSaveState _SaveState;
    public ItemSaveState SaveState => _SaveState;

    private void Awake()
    {
        if (_SaveState == null) 
            SetupItemSaveState();

        UpdateParent();
    }

    private void Start()
    {
        if(ItemID == null)
        {
            Debug.Log("O item <" + gameObject.name + "> Está sem ID, portanto precisa de um ID.");
        }
    }

    private void OnEnable()
    {
        _SaveState.IsActive = true;
    }

    private void OnDisable()
    {
        _SaveState.IsActive = false;
    }

    private void OnTransformParentChanged()
    {
        UpdateParent();
    }

    public void SetupItemSaveState()
    {
        _SaveState = new ItemSaveState(this);
    }

    public abstract void UseItem();

    public override void DoInteraction()
    {
        if (GameStateManager.Game.State != GameState.World_Free)
            return;

        R_Inventory.Instance.CollectItem(this);
    }

    protected virtual Useable_Object ClosestUseable()
    {
        Collider[] closeObjects = Physics.OverlapSphere(transform.position, 1.5f);
        List<Useable_Object> useableObjects = new List<Useable_Object>();

        bool haveUseableItem = false;

        for (int i = 0; i < closeObjects.Length; i++)
        {
            if (closeObjects[i].GetComponent<Useable_Object>() != null)
            {
                haveUseableItem = true;
                useableObjects.Add(closeObjects[i].GetComponent<Useable_Object>());
            }
        }

        if (!haveUseableItem)
            return null;

        int closestIndex = 0;

        for (int i = 0; i < useableObjects.Count; i++)
        {
            if (Vector3.Distance(transform.position, useableObjects[i].transform.position) < Vector3.Distance(transform.position, useableObjects[closestIndex].transform.position))
            {
                closestIndex = i;
            }
        }

        if (!useableObjects[closestIndex].CurrentState().CanBeUsed(this))
            return null;

        return useableObjects[closestIndex];
    }

    public void UpdateParent()
    {
        _SaveState.ItemParent = transform.parent;
    }

    protected virtual void RestoreItem()
    {
        transform.parent = _SaveState.ItemParent;
    }
}

[System.Serializable]
public class ItemSaveState
{
    public R_Item Item;
    public Transform ItemParent;
    public bool IsActive;

    public ItemSaveState(R_Item item)
    {
        Item = item;
    }
}