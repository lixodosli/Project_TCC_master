using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Interactable
{
    public override void DoInteraction()
    {
        int hourDiference = (24 - TimeManager.CurrentHour) + Despertador.HoursToWakeUp;
        
        Messenger.Broadcast(TimeManager.AdvanceTimeString, hourDiference);
        TimeManager.Instance.ExecuteEndOfDayFunctions();
    }
}