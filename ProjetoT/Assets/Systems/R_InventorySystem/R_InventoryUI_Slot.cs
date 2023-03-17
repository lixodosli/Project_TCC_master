using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class R_InventoryUI_Slot : MonoBehaviour
{
    #region Slot
    [SerializeField] private Image m_SlotBG;
    private bool _Selected;

    public void Select(bool select)
    {
        _Selected = select;

        UpdateSelection();
    }

    private void UpdateSelection()
    {
        if (_Selected)
        {
            m_SlotBG.color = ColorPicker.Instance.GetColor(ColorKey.Yolk);
        }
        else
        {
            m_SlotBG.color = ColorPicker.Instance.GetColor(ColorKey.Brick);
        }
    }
    #endregion;

    #region ItemInfo
    [SerializeField] private Image m_ItemIcon;
    [SerializeField] private Sprite m_BlankIcon;

    public void SetItem(R_Item item)
    {
        if (item == null)
        {
            m_ItemIcon.sprite = m_BlankIcon;
        }
        else
        {
            m_ItemIcon.sprite = item.ItemIcon;
        }
    }
    #endregion
}