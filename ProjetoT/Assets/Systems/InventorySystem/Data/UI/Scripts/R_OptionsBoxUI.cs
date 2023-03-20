using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_OptionsBoxUI : MonoBehaviour
{
    [SerializeField] private R_OptionsBoxUI_Line[] m_OptionsLines;
    public R_OptionsBoxUI_Line[] OptionsLines => m_OptionsLines;

    public void HighLightSelection(int highlight)
    {
        for (int i = 0; i < m_OptionsLines.Length; i++)
        {
            m_OptionsLines[i].Highlight(i == highlight);
        }
    }
}