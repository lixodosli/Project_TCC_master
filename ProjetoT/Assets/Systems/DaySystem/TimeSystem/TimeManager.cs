using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static string AdvanceTimeString = "Advance Time";
    public static int CurrentHour = 0;
    public static int CurrentDay = 1;

    private void Start()
    {
        CurrentHour = 8;
        CurrentDay = 1;

        Messenger.AddListener<int>(AdvanceTimeString, AdvanceTime);
    }

    public void AdvanceTime(int time)
    {
        CurrentHour += time;

        if(CurrentHour >= 24)
        {
            CurrentHour -= 24;
            CurrentDay++;
        }
    }

    private void OnGUI()
    {
        GUILayout.Label(string.Format("Day {0}, {1:00}:00", CurrentDay, CurrentHour));
    }
}