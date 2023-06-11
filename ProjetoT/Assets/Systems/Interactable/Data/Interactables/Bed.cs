using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Interactable
{
    public override void DoInteraction()
    {
        int hourDiference = (24 - TimeManager.CurrentHour) + Despertador.HoursToWakeUp;
        
        TimeManager.Instance.ExecuteEndOfDayFunctions();
        Messenger.Broadcast(TimeManager.AdvanceTimeString, hourDiference);
    }
}