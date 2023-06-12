using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private int _LastInput;

    private void Update()
    {
        UpdateDisplay();
    }

    private void LateUpdate()
    {
        _LastInput = (int)PlayerInputManager.Instance.PlayerInput.World.ChangeInventory.ReadValue<float>();
    }

    private void UpdateDisplay()
    {
        int inputChange = (int)PlayerInputManager.Instance.PlayerInput.World.ChangeInventory.ReadValue<float>();

        if(_LastInput !=  inputChange)
            ReorderPointer(inputChange);

        int pointerIndex = m_Pointer.GetSiblingIndex();

        if(pointerIndex >= m_Inventory.InventoryItem.Count)
        {
            m_ItemIconDisplay.sprite = m_DefaultIcon;
            m_ItemNameDisplay.text = "";
            return;
        }

        R_ItemConfigs item = m_Inventory.FindItem(pointerIndex);

        m_ItemIconDisplay.sprite = item != null ? item.ItemIcon : m_DefaultIcon;
        m_ItemNameDisplay.text = item != null ? item.ItemName : "";
    }

    private void ReorderPointer(int change)
    {
        int newIndex = m_Pointer.GetSiblingIndex() + change;

        if (newIndex >= m_Slots.childCount)
            newIndex = 0;
        else if (newIndex < 0)
            newIndex = m_Slots.childCount - 1;

        if (newIndex >= 0 && newIndex < m_Slots.childCount)
        {
            m_Pointer.SetSiblingIndex(newIndex);
        }
    }
}