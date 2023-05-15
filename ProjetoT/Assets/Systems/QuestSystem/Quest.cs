using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Quests")]
public class Quest : ScriptableObject
{
    public Quest PreviousQuestRequired;
    public string QuestName;
    public List<QuestStep> Steps = new List<QuestStep>();
}