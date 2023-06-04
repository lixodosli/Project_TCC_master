using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;
using UnityEditor.Rendering.LookDev;

public class R_InventoryDisplayer : MonoBehaviour
{
    [Header("Inventory To Display")]
    [SerializeField] private R_Inventory m_Inventory;

    [Header("Selection Elements")]
    [SerializeField] private Sprite m_DefaultIcon;
    [SerializeField] private Image m_ItemIconDisplay;
    [SerializeField] private TextMeshProUGUI m_ItemNameDisplay;
    [SerializeField] private Transform m_Pointer;
    [SerializeField] private Transform m_Slots;

    private void OnEnable()
    {
        PlayerInputManager.Instance.PlayerInput.World.Inventory.performed += CallForReorderPointer;
    }

    private void OnDisable()
    {
        PlayerInputManager.Instance.PlayerInput.World.Inventory.performed -= CallForReorderPointer;
    }

    private void Update()
    {
        UpdateDisplay();
    }

    private void CallForReorderPointer(InputAction.CallbackContext context)
    {
        ReorderPointer();
    }

    private void ReorderPointer()
    {
        int newIndex = m_Pointer.GetSiblingIndex() + 1;

        if (newIndex >= m_Slots.childCount)
            newIndex = 0;

        if (newIndex >= 0 && newIndex < m_Slots.childCount)
        {
            m_Pointer.SetSiblingIndex(newIndex);

            UpdateDisplay();
        }
    }

    private void UpdateDisplay()
    {
        int index = m_Pointer.GetSiblingIndex();

        if(index >= m_Inventory.InventoryItem.Count)
        {
            m_ItemIconDisplay.sprite = m_DefaultIcon;
            m_ItemNameDisplay.text = "";
            return;
        }

        R_ItemConfigs item = m_Inventory.FindItem(index);

        m_ItemIconDisplay.sprite = item != null ? item.ItemIcon : m_DefaultIcon;
        m_ItemNameDisplay.text = item != null ? item.ItemName : "";
    }
}