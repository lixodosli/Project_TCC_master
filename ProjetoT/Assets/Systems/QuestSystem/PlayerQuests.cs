using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuests : MonoBehaviour
{
    public static Dictionary<Quest, int> Quests = new Dictionary<Quest, int>();

    public static void AddQuest(Quest quest)
    {
        if (!HasCondition(quest))
            return;

        Quests.Add(quest, 0);
    }

    private static bool IsCompleted(Quest quest)
    {
        if (!Quests.ContainsKey(quest))
            return false;

        int q;
        Quests.TryGetValue(quest, out q);

        return q == quest.Steps.Count - 1;
    }

    private static bool HasCondition(Quest quest)
    {
        return Quests.ContainsKey(quest.PreviousQuestRequired) && IsCompleted(quest.PreviousQuestRequired);
    }

    public static void UpdateProgress(Quest quest)
    {
        if (!Quests.ContainsKey(quest))
            return;

        if (IsCompleted(quest))
            return;

        Quests[quest]++;
    }
}