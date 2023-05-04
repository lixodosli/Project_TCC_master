using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Useable : MonoBehaviour
{
    public string UseableName;
    public string SetName { get; protected set; }
    public bool ShowIndication = true;
    public bool UseItemTransition = true;

    public StatesConfig[] StatesConfigs;

    protected int _NextStageIndex = -1;

    private void OnEnable()
    {
        SetName = GetSetNameFromParent(transform.parent.gameObject);
        Debug.Log(SetName);
    }

    public string GetSetNameFromParent(GameObject gameObject)
    {
        string parent = "";
        GameObject go = gameObject;

        while(parent == "")
        {
            if (go == null)
                return "";

            Useable_Set set = go.GetComponent<Useable_Set>();
            if (set == null)
            {
                go = go.transform.parent.gameObject;
                continue;
            }
            else
            {
                parent = set.SetName;
                return parent;
            }
        }

        return parent;
    }

    public virtual bool CanBeUsed(Item item)
    {
        for (int i = 0; i < StatesConfigs.Length; i++)
        {
            if (StatesConfigs[i].ContainsItem(item) || item == null && StatesConfigs[i].ItemRequiredToInteract == null)
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
            return;

        if (_NextStageIndex < 0)
            return;

        if (!UseItemTransition)
            return;

        SendMessage(_NextStageIndex);
        Messenger.Broadcast<int>(TimeManager.AdvanceTimeString, StatesConfigs[_NextStageIndex].TimeToExecut);
    }

    public virtual void SendMessage(int index)
    {

        Messenger.Broadcast<string>(SetName, StatesConfigs[index].NextStage.UseableName);
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