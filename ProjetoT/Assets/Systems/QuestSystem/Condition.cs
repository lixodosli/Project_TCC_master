using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConditionType { Default, ByTimes, ByID }

[System.Serializable]
public class Condition
{
    public int Times;
    public string ID;
    public ConditionType Type;
    public bool ConditionCompleted { get; protected set; } = false;
    protected int _Counter = 0;

    public virtual void Start()
    {
        _Counter = 0;
    }

    public virtual bool IsComplete()
    {
        return ConditionCompleted;
    }
}

[System.Serializable]
public class UseSomeItem : Condition
{
    public Item ItemToUse;

    public override void Start()
    {
        _Counter = 0;
        ConditionCompleted = false;
        Inventory.Instance.OnUseItem += UpdateCondition;
    }

    public void UpdateCondition(Item item)
    {
        if(item.ItemName != ItemToUse.ItemName)
            return;

        switch (Type)
        {
            case ConditionType.Default:
                ConditionCompleted = true;
                break;

            case ConditionType.ByTimes:
                _Counter++;

                if (_Counter >= Times)
                {
                    Inventory.Instance.OnUseItem -= UpdateCondition;
                    ConditionCompleted = true;
                }
                Debug.Log($"Condition Updated by the item <{item.ItemName}>. Counter = {_Counter}/{Times}");
                break;

            case ConditionType.ByID:
                ConditionCompleted = true;
                break;
        }
    }
}

[System.Serializable]
public class InteractWithSomething : Condition
{
    public Interactable Interactable;

    public override void Start()
    {
        ConditionCompleted = false;
        InteractableInstigator.Instance.OnInteract += UpdateCondition;
    }

    public void UpdateCondition(Interactable interactable)
    {
        switch (Type)
        {
            case ConditionType.ByID:
                if(interactable.ItemID == ID)
                {
                    ConditionCompleted = true;
                    InteractableInstigator.Instance.OnInteract -= UpdateCondition;
                    return;
                }
                break;
        }
    }
}

[System.Serializable]
public class WaitForSomethihg : Condition
{

}