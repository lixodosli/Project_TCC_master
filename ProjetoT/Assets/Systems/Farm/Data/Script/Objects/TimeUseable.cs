using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TimeUseable : Useable
{
    [SerializeField] private List<TransformConfig> m_Configs = new List<TransformConfig>();

    public List<TimerEvent> Timers = new List<TimerEvent>();

    private void Start()
    {
        Messenger.AddListener<int>(TimeManager.AdvanceTimeString, UpdateCounting);
        Messenger.AddListener<string>(UseableName + SetName, UpdateTimers);
    }

    private void OnEnable()
    {
        // Remove any ended timer from the list
        Timers.RemoveAll(t => t.End);
        Timers.RemoveAll(t => !t.TrasnformationConfig.PauseWhenDisabled);

        // Start any unstarted timer from the configs
        foreach (TransformConfig config in m_Configs)
        {
            if (!Timers.Any(t => t.TrasnformationConfig == config))
            {
                Timers.Add(new TimerEvent(config, SetName, UseableName));
            }
        }

        // Start counting for all timers
        Timers.ForEach(t => t.StartCounting());
    }

    private void OnDisable()
    {
        for (int i = Timers.Count - 1; i >= 0; i--)
        {
            TimerEvent timer = Timers[i];

            if (timer.TrasnformationConfig.PauseWhenDisabled)
            {
                // Pause the timer
                timer.Pause(true);
            }
            else
            {
                // Remove the timer from the list
                timer.Pause(true);
                Timers.RemoveAt(i);
            }
        }
    }

    public override void Use(Item item)
    {
        if (!CanBeUsed(item) || _NextStageIndex < 0)
            return;

        Messenger.Broadcast<int>(TimeManager.AdvanceTimeString, StatesConfigs[_NextStageIndex].TimeToExecut);
    }

    private void UpdateCounting(int time)
    {
        Timers.ForEach(i => i.UpdateCounting());
    }

    private void UpdateTimers(string s)
    {
        Timers = Timers.Where(timer => !timer.End).ToList();
    }
}

[System.Serializable]
public class TransformConfig
{
    public Useable TransformTo;
    public int ChanceByWeight;
    public int TimeToTrigger;
    public bool PauseWhenDisabled;
}