using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Item), true)]
public class ItemEditor : Editor
{
    private Item item;

    private void OnEnable()
    {
        item = (Item)target;
    }

    public override void OnInspectorGUI()
    {
        //serializedObject.Update();

        //GUILayout.BeginHorizontal();

        //GUILayout.BeginVertical();
        EditorGUILayout.Space(20);
        item.ItemIcon = (Sprite)EditorGUILayout.ObjectField(item.ItemIcon, typeof(Sprite), true, GUILayout.Height(80), GUILayout.Width(80));
        //GUILayout.EndVertical();

        //GUILayout.BeginVertical();
        //EditorGUILayout.LabelField("Name:");
        //item.ItemName = EditorGUILayout.TextArea(item.ItemName, GUILayout.Height(20));
        //EditorGUILayout.LabelField("Description:");
        //item.ItemDescription = EditorGUILayout.TextArea(item.ItemDescription, GUILayout.Height(60));
        //GUILayout.EndVertical();

        //GUILayout.EndHorizontal();

        //EditorGUILayout.Separator();

        //GUILayout.BeginHorizontal();
        //item.HaveOnUse = EditorGUILayout.Toggle("On Use", item.HaveOnUse);
        //item.HaveOnCancel = EditorGUILayout.Toggle("On Cancel", item.HaveOnCancel);
        //GUILayout.EndHorizontal();

        //GUILayout.BeginHorizontal();
        //item.HaveOnSelect = EditorGUILayout.Toggle("On Select", item.HaveOnSelect);
        //item.HaveOnDiscard = EditorGUILayout.Toggle("On Discard", item.HaveOnDiscard);
        //GUILayout.EndHorizontal();

        //GUILayout.BeginHorizontal();
        //item.HaveOnGet = EditorGUILayout.Toggle("On Get", item.HaveOnGet);
        //GUILayout.EndHorizontal();

        //if (item.HaveOnUse)
        //{
        //    SerializedProperty onUseEvent = serializedObject.FindProperty("OnUse");
        //    EditorGUILayout.PropertyField(onUseEvent);
        //    serializedObject.ApplyModifiedProperties();
        //}
        //if (item.HaveOnCancel)
        //{
        //    SerializedProperty onUseEvent = serializedObject.FindProperty("OnCancel");
        //    EditorGUILayout.PropertyField(onUseEvent);
        //    serializedObject.ApplyModifiedProperties();
        //}
        //if (item.HaveOnSelect)
        //{
        //    SerializedProperty onUseEvent = serializedObject.FindProperty("OnSelect");
        //    EditorGUILayout.PropertyField(onUseEvent);
        //    serializedObject.ApplyModifiedProperties();
        //}
        //if (item.HaveOnDiscard)
        //{
        //    SerializedProperty onUseEvent = serializedObject.FindProperty("OnDiscard");
        //    EditorGUILayout.PropertyField(onUseEvent);
        //    serializedObject.ApplyModifiedProperties();
        //}
        //if (item.HaveOnGet)
        //{
        //    SerializedProperty onUseEvent = serializedObject.FindProperty("OnGet");
        //    EditorGUILayout.PropertyField(onUseEvent);
        //    serializedObject.ApplyModifiedProperties();
        //}

        base.OnInspectorGUI();
    }
}