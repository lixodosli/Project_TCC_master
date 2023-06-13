using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private TeleportConfig m_TeleportConfig;
    public int TimePass;

    public override void DoInteraction()
    {
        TeleportManager.Instance.RaiseTeleport(m_TeleportConfig);
        Messenger.Broadcast(TimeManager.AdvanceTimeString, TimePass);
    }
}