using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NPC))]
public class GenericNPCEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        NPC conv = (NPC)target;

        if(GUILayout.Button("Check Character Count"))
        {
            string[] texts = new string[conv.m_Conversations[0].Dialogues.Count];
            int[] charCount = new int[texts.Length];
            string d = "";

            for (int i = 0; i < conv.m_Conversations[0].Dialogues.Count; i++)
            {
                texts[i] = conv.m_Conversations[0].Dialogues[i].Text;
                charCount[i] = texts[i].Length;
                d += $"The Dialogue line '{i}' have: {charCount[i]} characters\n";
            }

            Debug.Log(d);
        }
    }
}