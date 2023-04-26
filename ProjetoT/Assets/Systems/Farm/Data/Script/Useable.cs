using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Useable : MonoBehaviour
{
    public string UseableName;
    public string SetName { get; private set; }

    public StatesConfig[] StatesConfigs;

    protected int _NextStageIndex = -1;

    private void Awake()
    {
        SetName = GetComponentInParent<Useable_Set>().SetName;
    }

    public virtual bool CanBeUsed(Item item)
    {
        for (int i = 0; i < StatesConfigs.Length; i++)
        {
            if (StatesConfigs[i].ContainsItem(item))
            {
                _NextStageIndex = i;
                return true;
            }
        }

        return false;
    }

    public virtual void Use(Item item)
    {
        if (!CanBeUsed(item))
        {
            return;
        }

        if(_NextStageIndex < 0)
        {
            return;
        }

        Messenger.Broadcast<string>(SetName, StatesConfigs[_NextStageIndex].NextStage.UseableName);
        Messenger.Broadcast<int>(TimeManager.AdvanceTimeString, StatesConfigs[_NextStageIndex].TimeToExecut);
    }
}

[System.Serializable]
public class StatesConfig
{
    public Item ItemRequiredToInteract;
    public Useable NextStage;
    public int TimeToExecut;

    public bool ContainsItem(Item item)
    {
        return ItemRequiredToInteract.ItemName == item.ItemName;
    }

    public bool ContainsState(Useable next)
    {
        return NextStage.UseableName == next.UseableName;
    }
}