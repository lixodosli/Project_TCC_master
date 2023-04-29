using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    public static string AdvanceTimeString = "Advance Time";
    public static string DayString = "Day Update";
    public static int CurrentHour = 0;
    public static int CurrentDay = 1;
    public static int TotalHours => CurrentDay * 24 + CurrentHour;

    private List<GEvent> _EndOfDayFunctions = new List<GEvent>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CurrentHour = 8;
        CurrentDay = 1;

        Messenger.AddListener<int>(AdvanceTimeString, AdvanceTime);
    }

    public void AddEvent(GEvent e)
    {
        _EndOfDayFunctions.Add(e);
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

    public void ExecuteEndOfDayFunctions()
    {
        SceneTransition.Instance.StartFadeIn();
        SceneTransition.Instance.OnSceneTransitionStart += StartEndOfDay;
    }

    private void StartEndOfDay()
    {
        SceneTransition.Instance.OnSceneTransitionStart -= StartEndOfDay;
        StartCoroutine(EndOfDayCoroutine());
    }

    private IEnumerator EndOfDayCoroutine()
    {
        foreach (GEvent func in _EndOfDayFunctions)
        {
            func.Execute();
            yield return null; // Wait one frame between functions
        }
    }

    private void OnGUI()
    {
        GUILayout.Label(string.Format("Day {0}, {1:00}:00", CurrentDay, CurrentHour));
    }
}