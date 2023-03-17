using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryItemOption : MonoBehaviour
{
    [SerializeField] private TMP_Text m_OptionText;
    [SerializeField] private GameObject m_Background;

    public void Setup(string text, bool visible)
    {
        m_OptionText.text = text;
        SetVisible(visible);
    }

    public void SetVisible(bool visible)
    {
        m_Background.SetActive(visible);
    }
}