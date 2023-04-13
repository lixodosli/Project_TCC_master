using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SheetsReader))]
public class SheetsReaderEditor : Editor
{
    SheetsReader reader;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        reader = (SheetsReader)target;

        GUILayout.BeginHorizontal();

        if(GUILayout.Button("Parse CSV"))
            reader.ParseCSV();

        GUILayout.EndHorizontal();
    }
}