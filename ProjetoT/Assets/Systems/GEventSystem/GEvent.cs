using System;
using UnityEngine;

public struct GEvent
{
    private readonly Action _action;

    public GEvent(Action action)
    {
        Debug.Log($"--- Criando o evento da acao <{action}> hihihi");
        _action = action;
    }

    public void Execute()
    {
        Debug.Log($"Invoking the action <{_action}> hihihi");
        _action?.Invoke();
    }
}