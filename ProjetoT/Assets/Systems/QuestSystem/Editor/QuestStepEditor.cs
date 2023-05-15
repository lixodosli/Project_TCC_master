using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuestStep))]
public class QuestStepEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        QuestStep step = (QuestStep)target;

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Use Item Condition"))
            step.AddUseItemCondition();

        if (GUILayout.Button("Add Interact Condition"))
            step.AddInteractCondition();

        if (GUILayout.Button("Add Wait Condition"))
            step.AddWaitCondition();

        if (GUILayout.Button("Reset Condition"))
            step.ResetCondition();

        GUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
}