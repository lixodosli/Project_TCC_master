using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePasser : MonoBehaviour
{
    public int TimeToPass;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
            Messenger.Broadcast(TimeManager.AdvanceTimeString, TimeToPass);
    }
}
