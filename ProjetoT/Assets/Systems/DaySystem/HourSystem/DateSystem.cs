using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateSystem : MonoBehaviour
{
    public static DateSystem Instance;

    public delegate void HourCallback(int hour);
    public HourCallback OnUpdateDate;
    public HourCallback OnUpdateHour;
    public HourCallback OnUpdateMinute;

    public delegate void DateCallback();
    public DateCallback OnEndDay;

    public bool _CanUpdateHour;
    public int _Date = 0;
    public int _Hour = 0;
    public int _Minute = 0;
    public bool CanUpdateHour => _CanUpdateHour;
    public int Date => _Date;
    public int Hour => _Hour;
    public int Minute => _Minute;

    private void Awake()
    {
        Instance = this;
        OnUpdateDate += UpdateDate;
    }

    private void OnDestroy()
    {
        OnUpdateDate -= UpdateDate;
    }

    private void Start()
    {
        _CanUpdateHour = true;
    }

    public void RaiseUpdateDate(int hour)
    {
        OnUpdateDate?.Invoke(hour);
    }

    private void UpdateDate(int time)
    {
        int newDate = _Date + time;

        if (newDate > 24)
        {
            _CanUpdateHour = false;
        }

        _Date += time;
        UpdateMinute(time);
    }

    private void UpdateMinute(int time)
    {
        int newMinute = _Minute + time;

        if(newMinute > 4)
        {
            UpdateHour(newMinute);
            _Minute = newMinute - 4;
        }
        else
        {
            _Minute = newMinute;
        }

        OnUpdateMinute?.Invoke(newMinute);
    }

    private void UpdateHour(int time)
    {
        int newHour = _Hour + time;

        if(newHour > 6)
        {
            _Hour = 0;
            // Terminou o dia
        }
        else
        {
            _Hour = newHour;
        }

        OnUpdateHour?.Invoke(newHour);
    }
}