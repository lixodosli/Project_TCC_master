using UnityEngine.Networking;
using System.Collections;
using System.IO;
using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CreateDialogues : MonoBehaviour
{
    public string tsvLink;
    public string targetFolderPath; // Specify the desired target folder path
    [TextArea(5, 50)] public string WhatIRead;

    IEnumerator FetchTSVData()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(tsvLink))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to fetch TSV data: " + www.error);
                yield break;
            }

            string tsvData = www.downloadHandler.text;
            WhatIRead = tsvData;

            // Parse the TSV data
            string[] lines = tsvData.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < lines.Length; i++) // Start from index 1 to skip the header row
            {
                string[] fields = lines[i].Split('\t'); // Split fields based on tabs ("\t")

                // Extract the values for FileName, NPCName, and Dialogue
                string fileName = fields[0];
                string npcName = fields[1];
                string dialogue = fields[2];

                // Create the asset file path
                string assetFileName = fileName + ".asset";
                string assetFilePath = Path.Combine(targetFolderPath, assetFileName);

                // Try to load the asset at the specified path
                SimpleDialogueNode dialogueNode = UnityEditor.AssetDatabase.LoadAssetAtPath<SimpleDialogueNode>(assetFilePath);

                // If the asset does not exist, create a new one
                if (dialogueNode == null)
                {
                    dialogueNode = ScriptableObject.CreateInstance<SimpleDialogueNode>();
                    UnityEditor.AssetDatabase.CreateAsset(dialogueNode, assetFilePath);
                }

                // Set the properties based on the extracted values
                dialogueNode.Name = npcName;
                dialogueNode.Text = dialogue;
                dialogueNode.LettersPerSecond = 0f;

                // Save the modifications
                UnityEditor.EditorUtility.SetDirty(dialogueNode);

                // Refresh the Asset Database
                UnityEditor.AssetDatabase.SaveAssets();
                UnityEditor.AssetDatabase.Refresh();
            }
        }
    }

    // Start the coroutine to fetch and process the TSV data
    [ContextMenu("Create Dialogues")]
    public void Create()
    {
        StartCoroutine(FetchTSVData());
    }
}
