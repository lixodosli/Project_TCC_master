using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject m_Indication;
    [SerializeField] private GameObject m_Display;
    [SerializeField] private Interactable m_Interactable;
    public bool IndicationActive => m_Indication.activeSelf;
    public bool DisplayActive => m_Display.activeSelf;

    public void ShowIndication(bool condition)
    {
        m_Indication.SetActive(condition);
    }

    public void ShowDisplay(bool condition)
    {
        m_Display.SetActive(condition);
    }

    public void DoInteraction()
    {
        m_Interactable.DoInteraction();
    }
}
