using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        //_Counter = 0;
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
                if (item.ItemID == ID)
                {
                    ConditionCompleted = true;
                    Inventory.Instance.OnUseItem -= UpdateCondition;
                    return;
                }
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

public enum WaitType { WaitTimeInDays, WaitTimeInHours };

[System.Serializable]
public class WaitForSomeTime : Condition
{
    public WaitType WaitType;
    private int StartHour;
    private int StartDay;

    public override void Start()
    {
        ConditionCompleted = false;
        StartHour = TimeManager.TotalHours;
        StartDay = TimeManager.CurrentDay;
        TimeManager.Instance.OnTotalHourChange += UpdateCondition;
    }

    public void UpdateCondition(int time)
    {
        bool conditionToEnd = WaitType == WaitType.WaitTimeInDays ? TimeManager.CurrentDay >= StartDay + Times : TimeManager.TotalHours >= StartHour + Times;

        if (conditionToEnd)
        {
            ConditionCompleted = true;
            TimeManager.Instance.OnTotalHourChange -= UpdateCondition;
        }
    }
}

[System.Serializable]
public class NeedSomeItem : Condition
{
    public Interactable PlaceToDelieverTheItems;
    public List<Item> ItemsNeeded = new List<Item>();

    public override void Start()
    {
        ConditionCompleted = false;
        InteractableInstigator.Instance.OnInteract += CheckInventory;
    }

    public void CheckInventory(Interactable deliver)
    {
        if (deliver.ItemName != PlaceToDelieverTheItems.ItemName)
            return;

        // Create a dictionary to store the count of each item in the Inventory
        Dictionary<Item, int> inventoryCount = new Dictionary<Item, int>();

        // Count the items in the Inventory
        for (int i = 0; i < Inventory.Instance.Items.Length; i++)
        {
            Item item = Inventory.Instance.Items[i];

            if (item == null)
                continue;

            if (inventoryCount.ContainsKey(item)) // I'M GETTING PROBLEM HERE
            {
                //Debug.Log($"Eu já tenho o item <{item.ItemName}> no dicio. Ele tem <{inventoryCount[item]}>.");
                inventoryCount[item]++;
            }
            else
            {
                inventoryCount[item] = 1;
                //inventoryCount[item] = 1;
            }
        }

        // Check if the Inventory contains every item in the ItemsNeeded array
        foreach (Item item in ItemsNeeded)
        {
            // If the item is not in the Inventory or the count is less than needed, return false
            if (!inventoryCount.ContainsKey(item))
            {
                ConditionCompleted = false;
                return;
            }
            else if (inventoryCount[item] == 0)
            {
                ConditionCompleted = false;
                return;
            }

            // Decrement the count of the item in the Inventory
            inventoryCount[item]--;
        }

        // If all items in ItemsNeeded are found in the Inventory, return true
        ConditionCompleted = true;
        InteractableInstigator.Instance.OnInteract -= CheckInventory;
    }

    private bool CheckKey(Item item, Dictionary<Item, int> dict)
    {
        foreach (KeyValuePair<Item, int> entry in dict)
        {
            Debug.Log($"{entry.Key}");
            //if (entry.Key.ItemName == item.ItemName)
            //    return true;
        }

        return false;
    }
}