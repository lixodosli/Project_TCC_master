using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerQuests : MonoBehaviour
{
    public static PlayerQuests Instance;
    public static string QuestTracker = "PlayerQuestTracker";
    public Dictionary<Quest, int> QuestsSteps = new Dictionary<Quest, int>();
    public List<Quest> QuestsList = new List<Quest>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddQuest(Quest quest)
    {
        if (!HasCondition(quest))
            return;

        Messenger.Broadcast<string>(QuestTracker, quest.QuestName);

        QuestsSteps.Add(quest, 0);
        QuestsList.Add(quest);

        Debug.Log($"<{QuestsList[QuestsList.Count - 1].QuestName}> + <{QuestsList[QuestsList.Count - 1].Steps[QuestsSteps[QuestsList[QuestsList.Count - 1]]].StepName}>");

        string debug = "";

        if (QuestsList.Last().Steps[QuestsSteps[quest]].Condition != null)
        {
            QuestsList.Last().Steps[QuestsSteps[quest]].Condition.Start();
            debug += "Condição iniciada!";
        }
        else debug += "Condição não encontrada.";

        Debug.Log(debug);
    }

    public bool IsCompleted(Quest quest)
    {
        if (!QuestsSteps.ContainsKey(quest))
            return false;

        int q;
        QuestsSteps.TryGetValue(quest, out q);

        bool condition = false;

        Condition c = QuestsList.Find(q => q.QuestName == quest.QuestName).Steps[q].Condition;

        if (c.IsComplete())
        {
            condition = true;
        }

        return q == quest.Steps.Count - 1 && condition;
    }

    private bool HasCondition(Quest quest)
    {
        if (quest.PreviousQuestRequired == null)
            return true;

        return QuestsSteps.ContainsKey(quest.PreviousQuestRequired) && IsCompleted(quest.PreviousQuestRequired);
    }

    public void UpdateProgress(Quest quest)
    {
        if (!QuestsSteps.ContainsKey(quest))
            return;

        if (IsCompleted(quest))
            return;

        QuestsSteps[quest]++;
    }
}