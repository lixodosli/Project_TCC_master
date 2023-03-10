using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [SerializeField] private GameObject m_InventoryUI;
    [SerializeField] private List<InventoryUI_Slot> m_Slots = new List<InventoryUI_Slot>();
    public bool Visible { get; private set; }

    private void Awake()
    {
        Instance = this;
        Visible = false;
        PlayerInputManager.Instance.PlayerInput.World.Inventory.performed += SetInventoryVisible;
    }

    private void OnDestroy()
    {
        PlayerInputManager.Instance.PlayerInput.World.Inventory.performed -= SetInventoryVisible;
    }

    private void Start()
    {
        SetupList();
        HideInventory();
    }

    private void SetupList()
    {
        m_Slots.Clear();
        InventoryUI_Slot[] slots = GetComponentsInChildren<InventoryUI_Slot>();

        foreach (InventoryUI_Slot item in slots)
        {
            item.SetupSlot();
            m_Slots.Add(item);
        }
    }

    public void AddItemInInventory(ItemSettings item, int index)
    {
        m_Slots[index].SetItem(item.Item, m_Slots[index].Quantity + 1);
        m_Slots[index].UpdateSlotElements();
    }

    public void SetInventoryVisible(InputAction.CallbackContext context)
    {
        if (Visible)
            HideInventory();
        else
            ShowInventory();
    }

    public void ShowInventory()
    {
        Visible = true;
        m_InventoryUI.SetActive(Visible);
    }

    public void HideInventory()
    {
        Visible = false;
        m_InventoryUI.SetActive(Visible);
    }

    public void SelectItem(int itemIndex)
    {
        for (int i = 0; i < m_Slots.Count; i++)
        {
            if(i == itemIndex)
                m_Slots[i].IsSelected = true;
            else
                m_Slots[i].IsSelected = false;

            m_Slots[i].UpdateSlotSelection();
        }
    }
}