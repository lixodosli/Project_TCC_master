using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Quests/Step")]
public class QuestStep : ScriptableObject
{
    public string StepName;
    [TextArea] public string StepDescription;
    //[SerializeReference] public List<Condition> Conditions;
    [SerializeReference] public Condition Condition;

    #region Menu
    public void AddUseItemCondition() { Condition = new UseSomeItem(); }
    public void AddInteractCondition() { Condition = new InteractWithSomething(); }
    public void AddWaitCondition() { Condition = new WaitForSomethihg(); }
    public void ResetCondition() { Condition = null; }
    #endregion
}