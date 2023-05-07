using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TimeUseable : Useable
{
    [SerializeField] private List<TransformConfig> m_TransformationByTimeConfigs = new List<TransformConfig>();

    public List<TimerEvent> Timers = new List<TimerEvent>();

    private void Awake()
    {
        Messenger.AddListener<string>(UseableName + SetName, UpdateTimers);
        Messenger.AddListener<int>(TimeManager.AdvanceTimeString, UpdateCounting);
    }

    private void OnEnable()
    {
        SetName = GetSetNameFromParent(transform.parent.gameObject);

        Debug.Log("ENABLE: Antes de Remover. \n" + DebugTimers());
        // Remove any ended timer from the list
        Timers.RemoveAll(t => t.End);
        Timers.RemoveAll(t => !t.TrasnformationConfig.PauseWhenDisabled);
        Debug.Log("ENABLE: Depois de Remover. \n" + DebugTimers());

        // Start any unstarted timer from the configs
        foreach (TransformConfig config in m_TransformationByTimeConfigs)
        {
            if (!Timers.Any(t => t.TrasnformationConfig == config))
            {
                Timers.Add(new TimerEvent(config, SetName, UseableName));
            }
        }

        Debug.Log("ENABLE: Depois de Adicionar. \n" + DebugTimers());
        // Start counting for all timers
        Timers.ForEach(t => t.StartCounting());
        Timers.ForEach(i => Debug.Log(i.DebugString()));
    }

    private void OnDisable()
    {
        Debug.Log("DISABLE: Antes de Iterar. \n" + DebugTimers());
        for (int i = Timers.Count - 1; i >= 0; i--)
        {
            if (Timers[i].TrasnformationConfig.PauseWhenDisabled)
            {
                // Pause the timer
                Timers[i].Pause(true);
            }
            else
            {
                // Remove the timer from the list
                Timers[i].Pause(true);
                Timers.RemoveAt(i);
            }
        }
        Debug.Log("DISABLE: Depois de Iterar. \n" + DebugTimers());
    }

    private void UpdateCounting(int time)
    {
        if (Timers.Count <= 0)
            return;

        Debug.Log("Dando Update na contagem da lista:\n" + DebugTimers());

        foreach (TimerEvent item in Timers)
        {
            item.UpdateCounting();
        }
    }

    private void UpdateTimers(string s)
    {
        TimerEvent timerToBeRemoved = null;

        foreach (TimerEvent item in Timers)
        {
            if (item.TrasnformationConfig.NextStage(false).UseableName == s)
            {
                timerToBeRemoved = item;
                break;
            }
        }

        Timers.Remove(timerToBeRemoved);

        Timers = Timers.Where(timer => !timer.End).ToList();
    }

    private string DebugTimers()
    {
        string d = $"Timers Size: {Timers.Count} \n";

        foreach (TimerEvent item in Timers)
        {
            d += $"-- <{item.UseableName}, {item.UseableSetName}> -> <{item.TrasnformationConfig.NextStage(false).UseableName}, {item.TrasnformationConfig.NextStage(false).SetName}>\n";
        }

        return d;
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