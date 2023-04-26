using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUseable : Useable
{
    public TransformationByTime Transformation { get; private set; }

    public bool StartWhenEnable = true;
    //public bool RestartWhenEnable = true;
    public bool PauseWhenDisable = true;
    public bool StopWhenDisable = true;
    public bool UseChances;
    [SerializeField] private List<TransformConfig> m_Configs = new List<TransformConfig>();

    public List<TimerEvent> Timers = new List<TimerEvent>();

    private void Awake()
    {
        Transformation = GetComponent<TransformationByTime>();
    }

    private void Start()
    {
        foreach (TransformConfig item in m_Configs)
        {
            Timers.Add(new TimerEvent(item, item.TransformTo.SetName, UseableName));
        }

        foreach (TimerEvent item in Timers)
        {
            item.StartCounting();
        }

        Messenger.AddListener<int>(TimeManager.AdvanceTimeString, UpdateCounting);
        Messenger.AddListener<string>(UseableName + SetName, UpdateTimers);
    }

    private void OnEnable()
    {
        if (StartWhenEnable)
        {
            Transformation.StartTrigger(UseChances? m_Configs[TransformIndex()] : m_Configs[0]);
        }

        if (PauseWhenDisable)
        {
            Transformation.Pause(false);
        }
    }

    private void OnDisable()
    {
        if (StopWhenDisable)
        {
            Transformation.StopTrigger();
        }

        if (PauseWhenDisable)
        {
            Transformation.Pause(true);
        }
    }

    public override void Use(Item item)
    {
        if (!CanBeUsed(item))
        {
            return;
        }

        if (_NextStageIndex < 0)
        {
            return;
        }

        Messenger.Broadcast<int>(TimeManager.AdvanceTimeString, StatesConfigs[_NextStageIndex].TimeToExecut);
    }

    private void UpdateCounting(int time)
    {
        foreach (TimerEvent timer in Timers)
        {
            timer.UpdateCounting();
        }
    }

    private void UpdateTimers(string s)
    {
        List<TimerEvent> timersToRemove = new List<TimerEvent>();

        foreach (TimerEvent timer in Timers)
        {
            if (timer.End)
                timersToRemove.Add(timer);
        }

        foreach (TimerEvent timerEvent in timersToRemove)
        {
            Timers.Remove(timerEvent);
        }
    }

    protected int TransformIndex()
    {
        int[] pile = new int[m_Configs.Count];

        for (int i = 0; i < m_Configs.Count; i++)
        {
            if (i == 0)
                pile[i] = m_Configs[i].ChanceByWeight;
            else
                pile[i] = m_Configs[i].ChanceByWeight + pile[i - 1];
        }

        int random = Random.Range(0, pile[pile.Length - 1]);
        int index = 0;

        for (int i = 0; i < pile.Length; i++)
        {
            if (random > pile[i])
            {
                index = i + 1;
            }
            else
            {
                index = i;
                break;
            }
        }

        return index;
    }
}

[System.Serializable]
public class TransformConfig
{
    public Useable TransformTo;
    public int ChanceByWeight;
    public int TimeToTrigger;
}