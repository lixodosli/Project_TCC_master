using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class R_InventoryUI_Slot : MonoBehaviour
{
    #region Slot
    [SerializeField] private Image m_SlotBG;
    private bool _Highlighted;
    public bool Highlighted => _Highlighted;
    #endregion;

    #region ItemInfo
    [SerializeField] private Image m_ItemIcon;
    [SerializeField] private Sprite m_BlankIcon;
    [SerializeField] private R_ToolTipUI m_ToolTip;
    [SerializeField] private R_OptionsBoxUI m_Options;
    public R_OptionsBoxUI Options => m_Options;
    private R_Item _ItemInSlot;
    public R_Item ItemInSlot => _ItemInSlot;
    #endregion;

    #region Slot
    public void HighlightSlot(bool highlight)
    {
        _Highlighted = highlight;

        if (_ItemInSlot)
        {
            m_ToolTip.gameObject.SetActive(highlight);
            m_Options.gameObject.SetActive(highlight);
        }
        else
        {
            m_ToolTip.gameObject.SetActive(false);
            m_Options.gameObject.SetActive(false);
        }

        UpdateHighlight();
    }

    private void UpdateHighlight()
    {
        if (_Highlighted)
        {
            m_SlotBG.color = ColorPicker.Instance.GetColor(ColorKey.Yolk);

            if(_ItemInSlot != null)
            {
                m_ToolTip.SetToolTip(_ItemInSlot.ItemName);
                m_ToolTip.gameObject.SetActive(true);
            }
        }
        else
        {
            m_SlotBG.color = ColorPicker.Instance.GetColor(ColorKey.Brick);

            if(_ItemInSlot != null)
                m_ToolTip.gameObject.SetActive(false);
        }
    }
    #endregion;

    #region ItemInfo
    public void SetItem(R_Item item)
    {
        if (item == null)
        {
            m_ItemIcon.sprite = m_BlankIcon;
            _ItemInSlot = null;
        }
        else
        {
            m_ItemIcon.sprite = item.ItemIcon;
            _ItemInSlot = item;
        }
    }
    #endregion
}