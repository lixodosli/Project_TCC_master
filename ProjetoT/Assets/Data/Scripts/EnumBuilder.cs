using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnumBuilder : MonoBehaviour
{
    [SerializeField] private string m_EnumName;
    [SerializeField] private string m_FilePath;

    [ContextMenu("Generate Enum")]
    public void CreateNewEnum()
    {
        //ItemSaver.Instance.SetupData();
        string newEnum = "public enum " + m_EnumName + " {\n";
        Dictionary<int, string> myValues = ItemManager.ItemList;
        string value;

        for (int i = 0; i < myValues.Count; i++)
        {
            value = myValues[i].Replace(" ", "");

            newEnum += value + ",\n";
        }

        newEnum += "}";

        File.WriteAllText(m_FilePath, newEnum);
    }
}