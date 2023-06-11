using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
public class RenamerToolWindow : EditorWindow
{
    UnityEngine.Object[] selectedObjects;
    string wantedPrefix = "";
    string wantedName = "";
    string wantedSuffix = "";
    string originalWord = "";
    string newWord = "";
    bool addNumbering;
    //bool initDone = false;
    string finalName = String.Empty;
    Vector2 scrollPosition = Vector2.zero;
    GUIStyle yellowText;
    [MenuItem("Tools/Rename Assets")]
    public static void LaunchRenamer()
    {
        var win = GetWindow<RenamerToolWindow>();
        GUIContent content = new GUIContent("Rename Objects");
        win.titleContent = content;
        win.Show();
    }
    void OnEnable()
    {
        yellowText = new GUIStyle(EditorStyles.label);
        yellowText.normal.textColor = Color.yellow;
    }
    private void OnGUI()
    {
        selectedObjects = Selection.objects;
        EditorGUILayout.LabelField("Selected: " + selectedObjects.Length.ToString("000"));
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(10);
        EditorGUILayout.BeginVertical();
        GUILayout.Space(10);
        wantedPrefix = EditorGUILayout.TextField("Prefix: ", wantedPrefix);
        wantedName = EditorGUILayout.TextField("Name: ", wantedName);
        wantedSuffix = EditorGUILayout.TextField("Suffix: ", wantedSuffix);
        using (var horizontalScope = new GUILayout.HorizontalScope(GUILayout.Height(20)))
        {
            originalWord = EditorGUILayout.TextField("Replace ", originalWord);
            EditorGUILayout.LabelField(" with  ", GUILayout.Width(30));
            newWord = EditorGUILayout.TextField(newWord);
        }
        addNumbering = EditorGUILayout.Toggle("Add Numbering? ", addNumbering);
        GUILayout.Space(10);
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(10);
        EditorGUILayout.EndVertical();
        if (GUILayout.Button("Rename selected objects."))
        {
            SaveRenames();
        }
        EditorGUILayout.HelpBox("Remember to test after renaming, since some objects in game have their names hardcoded.", MessageType.Warning);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        Repaint();
        EditorGUILayout.LabelField("Preview:");
        PreviewRename();
        GUILayout.EndScrollView();
    }
    void PreviewRename()
    {
        Array.Sort(selectedObjects, delegate (UnityEngine.Object objectA, UnityEngine.Object objectB) { return objectA.name.CompareTo(objectB.name); });
        for (int i = 0; i < selectedObjects.Length; i++)
        {
            var initialName = selectedObjects[i].name;
            finalName = String.Empty;
            if (wantedPrefix != "")
            {
                finalName += wantedPrefix;
            }
            if (wantedName != "")
            {
                finalName += wantedName;
            }
            else
            {
                finalName += selectedObjects[i].name;
                if (originalWord != "" && newWord != "" && selectedObjects[i].name.Contains(originalWord))
                {
                    finalName = finalName.Replace(originalWord, newWord);
                }
            }
            if (wantedSuffix != "")
            {
                finalName += wantedSuffix;
            }
            if (addNumbering == true)
            {
                finalName += i.ToString("_00");
            }
            using (var horizontalScope = new GUILayout.HorizontalScope(GUILayout.Height(20)))
            {
                EditorGUILayout.LabelField(initialName, GUILayout.Width(300));
                EditorGUILayout.LabelField(" ==> ", yellowText, GUILayout.Width(40));
                EditorGUILayout.LabelField(finalName);
            }
        }
    }
    void SaveRenames()
    {
        for (int i = 0; i < selectedObjects.Length; i++)
        {
            finalName = String.Empty;
            if (wantedPrefix != "")
            {
                finalName += wantedPrefix;
            }
            if (wantedName != "")
            {
                finalName += wantedName;
            }
            else
            {
                finalName += selectedObjects[i].name;
                if (originalWord != "" && newWord != "" && selectedObjects[i].name.Contains(originalWord))
                {
                    finalName = finalName.Replace(originalWord, newWord);
                }
            }
            if (wantedSuffix != "")
            {
                finalName += wantedSuffix;
            }
            if (addNumbering == true)
            {
                finalName += i.ToString("_00");
            }
            if (selectedObjects[i].name.ToUpper() == finalName.ToUpper())
            {
                Debug.LogError("Error on Item " + selectedObjects[i].name + " you can't only change the capitalization in the name of an Item.");
            }
            else
            {
                selectedObjects[i].name = finalName;
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(selectedObjects[i]), finalName);
            }
        }
    }
}