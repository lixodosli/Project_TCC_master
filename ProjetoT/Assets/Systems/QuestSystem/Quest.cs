using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Quests")]
public class Quest : ScriptableObject
{
    public Quest PreviousQuestRequired;
    public string QuestName;
    public List<QuestSteps> Steps = new List<QuestSteps>();
}

[System.Serializable]
public class QuestSteps
{
    public string StepName;
    [TextArea] public string StepDescription;
}