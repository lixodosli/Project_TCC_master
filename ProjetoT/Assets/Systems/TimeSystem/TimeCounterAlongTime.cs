public class TimeCounterAlongTime
{
    public string TimerName;
    public int Duration { get; private set; }
    private int StartHour;
    private int _Counter;
    public bool Achieve { get; private set; }

    public TimeCounterAlongTime(string timerName, int duration)
    {
        TimerName = timerName;
        StartHour = TimeManager.TotalHours;
        Duration = duration;
        _Counter = 0;
    }

    public void UpdateCounter (int time)
    {
        int deltaTime = time - StartHour;
        _Counter += deltaTime;
        Achieve = _Counter >= Duration;
    }
}