 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFeitoNasCoxa : NPC
{
    public static NPCFeitoNasCoxa Instance;
    public bool HaveCondition1 = false;
    public bool HaveCondition2 = false;
    public Transform JudasSpot;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
    }

    public override void DoInteraction()
    {
        //VerifyConditions(m_CurrentConversationIndex);

        base.DoInteraction();
    }

    public void VerifyConditions(int index)
    {
        switch (index)
        {
            default:
            case 0:
                break;
            case 1:
                if (HaveCondition1)
                    ChangeNarrative(2);
                break;
            case 3:
                if (HaveCondition2)
                    ChangeNarrative(4);
                break;
        }
    }

    public void VaiPraOndeJudasPerdeuAsBotas()
    {
        transform.position = JudasSpot.position;
    }
}
