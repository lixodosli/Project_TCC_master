using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Interactable
{
    public override void DoInteraction()
    {
        DaySystem.Instance.RaiseEndDay(DaySystem.Instance.DayCount);
    }
}