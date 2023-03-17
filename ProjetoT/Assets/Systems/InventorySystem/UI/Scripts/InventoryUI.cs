using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    public delegate void InventoryUICallback(bool visible);
    public InventoryUICallback OnCallInventory;

    [SerializeField] private GameObject m_InventoryUI;
    [SerializeField] private List<InventoryUI_Slot> m_Slots = new List<InventoryUI_Slot>();
    public int OptionSelected;
    public bool Visible { get; private set; }

    private void Awake()
    {
        Instance = this;
        Visible = false;
        PlayerInputManager.Instance.PlayerInput.World.Inventory.performed += SetInventoryVisible;
        PlayerInputManager.Instance.PlayerInput.World.InventoryNavigateDown.performed += NavigateSelectionDown;
        PlayerInputManager.Instance.PlayerInput.World.InventoryNavigateUp.performed += NavigateSelectionUp;
    }

    private void OnDestroy()
    {
        PlayerInputManager.Instance.PlayerInput.World.Inventory.performed -= SetInventoryVisible;
        PlayerInputManager.Instance.PlayerInput.World.InventoryNavigateDown.performed -= NavigateSelectionDown;
        PlayerInputManager.Instance.PlayerInput.World.InventoryNavigateUp.performed -= NavigateSelectionUp;
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

    private void NavigateSelectionDown(InputAction.CallbackContext context)
    {
        SetOptionSelection(1);
    }

    private void NavigateSelectionUp(InputAction.CallbackContext context)
    {
        SetOptionSelection(0);
    }

    public void SetOptionSelection(int selection)
    {
        OptionSelected = Mathf.Clamp(selection, 0, 1);

        for (int i = 0; i < m_Slots.Count; i++)
        {
            m_Slots[i].UpdateOptionSelection(selection);
        }
    }

    public void AddItemInInventory(ItemSettings item, int index)
    {
        m_Slots[index].SetItem(item.Item);
        m_Slots[index].UpdateSlotElements();
    }

    public void RemoveItemInInventory(int index)
    {
        m_Slots[index].SetupSlot();
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

        for (int i = 0; i < m_Slots.Count; i++)
        {
            m_Slots[i].UpdateSlotSelection();
            m_Slots[i].UpdateSlotElements();
        }

        SetOptionSelection(0);
        SelectItem(Inventory.Instance.SelectedItemIndex());

        OnCallInventory?.Invoke(Visible);
    }

    public void HideInventory()
    {
        Visible = false;
        for (int i = 0; i < m_Slots.Count; i++)
        {
            m_Slots[i].UpdateSlotElements();
        }
        m_InventoryUI.SetActive(Visible);
        OnCallInventory?.Invoke(Visible);
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