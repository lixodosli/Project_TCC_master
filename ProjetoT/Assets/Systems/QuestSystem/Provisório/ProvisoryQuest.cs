using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvisoryQuest : MonoBehaviour
{
    public NPC npc;
    public Quest Quest1;
    public bool tracking;

    private void Start()
    {
        Messenger.AddListener<string>(PlayerQuests.QuestTracker, StartQuestTracking);
    }

    private void Update()
    {
        
    }

    private void StartQuestTracking(string quest)
    {
        if(quest == Quest1.QuestName)
            tracking = true;

        npc.ChangeNarrative(1);
    }
}