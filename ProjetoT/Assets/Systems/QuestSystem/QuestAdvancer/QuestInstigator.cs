using UnityEngine;

public abstract class QuestInstigator : ScriptableObject
{
    public abstract bool CanUpdateQuestProgress();
    public abstract void UpdateQuestProgress(Quest quest);
}