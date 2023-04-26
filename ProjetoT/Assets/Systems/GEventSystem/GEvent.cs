using System;
using UnityEngine;

public class GEvent 
{
    private readonly Action _action;

    public GEvent(Action action)
    {
        DaySystem.Instance.OnDayEnd += Execute;
        _action = action;
    }

    public void Execute(int day)
    {
        DaySystem.Instance.OnDayEnd -= Execute;
        _action?.Invoke();
    }
}