using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerEvent
{
    public TransformConfig Config { get; private set; }
    public string UseableSetName { get; private set; }
    public string UseableName { get; private set; }
    public bool Paused { get; private set; }
    public bool End { get; private set; }

    private int _Counter;
    private int _LastUpdate;
    private bool _Counting;

    public TimerEvent(TransformConfig config, string setName, string useAbleName)
    {
        Config = config;
        UseableSetName = setName;
        UseableName = useAbleName;
    }

    public void StartCounting()
    {
        _Counter = 0;
        _LastUpdate = TimeManager.TotalHours;
        _Counting = true;
    }

    public void UpdateCounting()
    {
        if (Paused)
        {
            _LastUpdate = TimeManager.TotalHours;
            return;
        }

        if (!_Counting)
            return;

        _Counter += TimeManager.TotalHours - _LastUpdate;
        _LastUpdate = TimeManager.TotalHours;

        if(_Counter >= Config.TimeToTrigger)
        {
            StopCounting();
        }
    }

    public void StopCounting()
    {
        Messenger.Broadcast<string>(UseableName + UseableSetName, Config.TransformTo.UseableName);
        Messenger.Broadcast<string>(UseableSetName, Config.TransformTo.UseableName);
        _Counting = false;
    }

    public void Pause(bool pause)
    {
        Paused = pause;
    }
}