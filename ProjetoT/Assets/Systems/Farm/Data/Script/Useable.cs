using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Useable : MonoBehaviour
{
    public string UseableName;
    public string SetName { get; private set; }
    protected Useable_Object _Object;

    public StatesConfig[] StatesConfigs;

    private int _NextStageIndex = -1;

    private void Awake()
    {
        SetName = transform.parent.GetComponent<Useable_Set>().SetName;
    }

    public virtual void SetupObj(Useable_Object obj)
    {
        _Object = obj;
    }

    public virtual bool CanBeFollowedByState(Useable state)
    {
        //for (int i = 0; i < StatesConfigs.Length; i++)
        //{
        //    if (StatesConfigs[i].ContainsState(state))
        //    {
        //        _NextStageIndex = i;
        //        return true;
        //    }
        //}

        return false;
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

    public virtual void OnUsed(Item item)
    {
        UseableManager.Instance.RequestChangeState(new UseableObjInfo(StatesConfigs[0].NextStage, _Object));
    }

    public virtual void Use(Item item)
    {
        if (!CanBeUsed(item))
        {
            Debug.Log("N Posso usar o item");
            return;
        }

        if(_NextStageIndex < 0)
        {
            Debug.Log("N deu");
            return;
        }

        Debug.Log(_NextStageIndex);

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