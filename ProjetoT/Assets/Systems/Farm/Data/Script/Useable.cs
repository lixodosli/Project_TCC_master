using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Useable : MonoBehaviour
{
    protected Useable_Object _Object;

    public List<R_Item> Interact = new List<R_Item>();
    public List<Useable> PossibleNextStates = new List<Useable>();

    public virtual void SetupObj(Useable_Object obj)
    {
        _Object = obj;
    }

    public virtual bool CanBeFollowedByState(Useable state)
    {
        bool canFollow = false;

        for (int i = 0; i < PossibleNextStates.Count; i++)
        {
            if (PossibleNextStates[i] == state)
            {
                canFollow = true;
                break;
            }
        }

        return canFollow;
    }

    public virtual bool CanBeUsed(R_Item item)
    {
        bool canUse = false;

        for (int i = 0; i < Interact.Count; i++)
        {
            if(Interact[i].ItemName == item.ItemName)
            {
                canUse = true;
                break;
            }
        }

        return canUse;
    }

    public virtual void OnUsed(R_Item item)
    {
        UseableManager.Instance.RequestChangeState(new UseableObjInfo(PossibleNextStates[0], _Object));
    }
}