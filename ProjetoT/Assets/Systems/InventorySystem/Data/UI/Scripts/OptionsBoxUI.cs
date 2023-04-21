using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsBoxUI : MonoBehaviour
{
    [SerializeField] private OptionsBoxUI_Line[] m_OptionsLines;
    public OptionsBoxUI_Line[] OptionsLines => m_OptionsLines;

    public void HighLightSelection(int highlight)
    {
        for (int i = 0; i < m_OptionsLines.Length; i++)
        {
            m_OptionsLines[i].Highlight(i == highlight);
        }
    }
}