using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin_Seed : Useable
{
    [SerializeField] private int m_TimeToExecute;

    public override void OnUsed(R_Item item)
    {
        DateSystem.Instance.RaiseUpdateDate(m_TimeToExecute);
        base.OnUsed(item);
    }
}