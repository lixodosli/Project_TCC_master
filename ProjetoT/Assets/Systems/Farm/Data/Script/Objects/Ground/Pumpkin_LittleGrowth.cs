using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin_LittleGrowth : Useable
{
    [SerializeField] private int m_DaysToGrowth;
    private int _DayStart;

    private void Awake()
    {
        DaySystem.Instance.OnDayEnd += Growth;
    }

    private void OnDestroy()
    {
        DaySystem.Instance.OnDayEnd -= Growth;
    }

    private void OnEnable()
    {
        _DayStart = DaySystem.Instance.DayCount;
    }

    public void Growth(int day)
    {
        if(day >= _DayStart + m_DaysToGrowth)
        {
            OnUsed(null);
        }
    }
}