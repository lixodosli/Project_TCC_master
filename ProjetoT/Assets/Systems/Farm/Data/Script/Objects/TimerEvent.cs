using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimerEvent
{
    public TransformConfig TrasnformationConfig;
    public string UseableSetName { get; private set; }
    public string UseableName { get; private set; }
    public bool Paused { get; private set; }
    public bool End { get; private set; }

    private int _Counter;
    private int _LastUpdate;
    private bool _Counting;

    public TimerEvent(TransformConfig transformationConfig, string setName, string useAbleName)
    {
        TrasnformationConfig = transformationConfig;
        UseableSetName = setName;
        UseableName = useAbleName;
    }

    public string DebugString() => $"<{UseableName}, {UseableSetName}> || {_Counter}/{TrasnformationConfig.TimeToTrigger}";

    public void StartCounting()
    {
        if (Paused)
        {
            Paused = false;
            return;
        }

        _Counter = 0;
        _LastUpdate = TimeManager.TotalHours;
        _Counting = true;
        End = false;
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

        Debug.Log(DebugString());

        if(_Counter >= TrasnformationConfig.TimeToTrigger)
        {
            StopCounting();
        }
    }

    public void StopCounting()
    {
        End = true;
        Debug.Log($"The timer of <{UseableSetName}, {UseableName}> is trying to end and send the messages.");
        Messenger.Broadcast<string>(UseableName + UseableSetName, TrasnformationConfig.NextStage(false).UseableName);
        Messenger.Broadcast<string>(UseableSetName, TrasnformationConfig.NextStage(false).UseableName);
        _Counting = false;
    }

    public void Pause(bool pause)
    {
        Paused = pause;
    }
}