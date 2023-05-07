using UnityEngine;

public class Counter
{
    private string _CounterName;
    public string CounterName => _CounterName;
    private bool _IsCounting;
    private float _Counter;
    private float _PercentageComplete;
    public float PercentageComplete => _PercentageComplete;
    private float _Time;

    public Counter(string counterName, float time)
    {
        _CounterName = counterName;
        _Time = time;
    }

    public void Play()
    {
        _IsCounting = true;
    }

    public void Pause()
    {
        _IsCounting = false;
    }

    public void Stop()
    {
        Pause();
        _Counter = 0;
        _PercentageComplete = 1;
    }

    public void Update()
    {
        if (!_IsCounting)
            return;

        _Counter += Time.deltaTime;
        _PercentageComplete = _Counter / _Time;

        if(_PercentageComplete >= 1)
        {
            Messenger.Broadcast(_CounterName, true);
            Stop();
        }
    }
}