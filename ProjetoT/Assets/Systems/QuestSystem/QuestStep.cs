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
    [ContextMenu("Add Use Item Condition")] public void AddUseItemCondition() { Condition = new UseSomeItem(); }
    [ContextMenu("Add Do Interaction Condition")] public void AddInteractCondition() { Condition = new InteractWithSomething(); }
    [ContextMenu("Add Wait Condition")] public void AddWaitCondition() { Condition = new WaitForSomeTime(); }
    [ContextMenu("Add Need Some Item Condition")] public void AddNeedSomeItem() { Condition = new NeedSomeItem(); }
    [ContextMenu("Reset Condition")] public void ResetCondition() { Condition = null; }
    #endregion
}