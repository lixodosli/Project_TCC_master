using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InteractableItem))]
public class InteractableItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        InteractableItem item = (InteractableItem)target;

        GUILayout.BeginHorizontal();

        if(GUILayout.Button("Setup Tip Box"))
            item.SetupTipBox();

        if (GUILayout.Button("Setup Tip Position"))
            item.SetupTipPosition();

        if (GUILayout.Button("Generate ItemID"))
            item.GenerateItemID();

        GUILayout.EndHorizontal();
    }
}