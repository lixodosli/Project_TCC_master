using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuests : MonoBehaviour
{
    public static PlayerQuests Instance;
    public static string QuestTracker = "PlayerQuestTracker";
    public Dictionary<Quest, int> Quests = new Dictionary<Quest, int>();

    public delegate void PlayerQuestsDelegate(Quest quest);
    public PlayerQuestsDelegate OnCallForSomething;

    private void Awake()
    {
        Instance = this;
    }

    public void AddQuest(Quest quest)
    {
        if (!HasCondition(quest))
            return;

        Messenger.Broadcast<string>(QuestTracker, quest.QuestName);
        Quests.Add(quest, 0);
    }

    private bool IsCompleted(Quest quest)
    {
        if (!Quests.ContainsKey(quest))
            return false;

        int q;
        Quests.TryGetValue(quest, out q);

        return q == quest.Steps.Count - 1;
    }

    private bool HasCondition(Quest quest)
    {
        if (quest.PreviousQuestRequired == null)
            return true;

        return Quests.ContainsKey(quest.PreviousQuestRequired) && IsCompleted(quest.PreviousQuestRequired);
    }

    public void UpdateProgress(Quest quest)
    {
        if (!Quests.ContainsKey(quest))
            return;

        if (IsCompleted(quest))
            return;

        Quests[quest]++;
    }
}