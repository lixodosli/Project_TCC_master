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
    public TransformToConfig[] Transformations;
    public int TimeToTrigger;
    public bool PauseWhenDisabled;

    public Useable NextStage()
    {
        return Transformations[0].Transformation;
    }

    public Useable NextStage(int index)
    {
        return Transformations[index].Transformation;
    }

    public Useable NextStage(bool random)
    {
        if (random)
        {
            // Return a Useable of a totaly random Transformation index.
            int randomIndex = Random.Range(0, Transformations.Length);
            return Transformations[randomIndex].Transformation;
        }
        else
        {
            // Return a Useable of a random Transformation index, calculated by the wheights defined by each one.
            int totalWeight = Transformations.Sum(t => t.Wheight);
            int randomValue = Random.Range(0, totalWeight);
            int currentIndex = 0;

            foreach (var transformation in Transformations)
            {
                currentIndex += transformation.Wheight;
                if (randomValue < currentIndex)
                {
                    return transformation.Transformation;
                }
            }
        }

        // If no valid Useable was found, return null.
        return null;
    }
}

[System.Serializable]
public struct TransformToConfig
{
    public Useable Transformation;
    public int Wheight;
}