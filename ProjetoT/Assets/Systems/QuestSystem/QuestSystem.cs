using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    private List<Quest> _Quests = new List<Quest>();

    private void Awake()
    {
        LoadQuests();
    }

    public void LoadQuests()
    {
        Quest[] quests = Resources.LoadAll<Quest>("Quests");
        foreach (Quest q in quests)
        {
            _Quests.Add(q);
        }
    }
}