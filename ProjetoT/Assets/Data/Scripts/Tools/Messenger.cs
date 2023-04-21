using System;
using System.Collections.Generic;

public static class Messenger
{
    private static Dictionary<string, Delegate> eventTable = new Dictionary<string, Delegate>();

    public static void AddListener<T>(string eventType, Action<T> handler)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            eventTable.Add(eventType, null);
        }
        eventTable[eventType] = (Action<T>)eventTable[eventType] + handler;
    }

    public static void RemoveListener<T>(string eventType, Action<T> handler)
    {
        if (eventTable.ContainsKey(eventType))
        {
            eventTable[eventType] = (Action<T>)eventTable[eventType] - handler;
        }
    }

    public static void Broadcast<T>(string eventType, T arg1)
    {
        Delegate d;
        if (eventTable.TryGetValue(eventType, out d))
        {
            Action<T> callback = d as Action<T>;
            if (callback != null)
            {
                callback(arg1);
            }
            else
            {
                throw new InvalidOperationException("Broadcasting message with wrong type.");
            }
        }
    }

    // You can add more Broadcast and AddListener methods here for different numbers of parameters
}