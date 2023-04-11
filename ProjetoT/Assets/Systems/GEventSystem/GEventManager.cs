using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEventManager : MonoBehaviour
{
    public static GEventManager Instance { get; private set; }

    public delegate void GEventCallback();
    public GEventCallback OnExecuteGEvent;

    private List<GEvent> _GEvents = new List<GEvent>();
    public List<GEvent> GEvents => _GEvents;

    private void Awake()
    {
        Instance = this;
    }

    public void AddGEvent(GEvent gEvent)
    {
        _GEvents.Add(gEvent);
    }

    public void ExecuteGEvent()
    {
        foreach (GEvent evt in _GEvents)
        {
            evt.Execute(DaySystem.Instance.DayCount);
            _GEvents.Remove(evt);
        }
    }
}