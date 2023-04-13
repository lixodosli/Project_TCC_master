using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

public class SheetsReader : MonoBehaviour
{
    [Header("Writer")]
    [SerializeField] private string m_Url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vSd-0RMMOee109yHE1Hemu5Ev0n5URjhidI1UfJRMm-P4fljt6cNiK0j4aRcJ7FhCZJknyvHzNhVlZH/pub?gid=0&single=true&output=csv";
    [SerializeField] private string m_SavePath = "Assets/Data/data.json";

    private WebClient _Client = new WebClient();
    private Data _GameData = new Data();

    public void ParseCSV()
    {
        _GameData.entries.Clear();
        string debug = "== Starting Proccess ==\n";
        string csv = _Client.DownloadString(m_Url);

        string[] lines = csv.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');
            _GameData.entries.Add(new Entry(values[0], values[1]));

            for (int j = 0; j < values.Length; j++)
            {
                values[j].Trim();
            }

            debug += "Adding the entry: " + values[0] + ": " + values[1] + "\n";
        }

        string json = JsonUtility.ToJson(_GameData);

        File.WriteAllText(m_SavePath, json, Encoding.UTF8);
        debug += "== Done ==";
        Debug.Log(debug);
    }

    public static T ReadJSON<T>(string fileText)
    {
        T data = JsonUtility.FromJson<T>(fileText);

        return data;
    }

    [System.Serializable]
    private class Reader
    {

    }

    [System.Serializable]
    private class Data
    {
        public List<Entry> entries = new List<Entry>();
    }

    [System.Serializable]
    private class Entry
    {
        public string name;
        public string value;

        public Entry(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
    }
}