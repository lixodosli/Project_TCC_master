using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    #region Singleton
    public static InventoryUI Instance;
    #endregion

    #region Elementos
    [SerializeField] private GameObject m_InventoryUIElements;
    [SerializeField] private InventoryUI_Slot[] m_Slots = new InventoryUI_Slot[9];
    public InventoryUI_Slot[] Slots => m_Slots;
    #endregion

    #region Caracteristicas
    [SerializeField] private bool m_Visible;
    public bool Visible => m_Visible;
    #endregion

    private void Awake()
    {
        Instance = this;
        Inventory.Instance.OnOpenCloseInventory += DoInventoryDisplay;
        Inventory.Instance.OnChangeSelectedItem += UpdateSelection;
        Inventory.Instance.OnChangeSelectedOption += UpdateOption;
        Inventory.Instance.OnCollectItemUI += DoInventoryItemDisplay;
        Inventory.Instance.OnDropItem += DoRemoveItemInventory;
        Inventory.Instance.OnConsumeItem += DoRemoveItemInventory;
    }

    private void OnDestroy()
    {
        Inventory.Instance.OnOpenCloseInventory -= DoInventoryDisplay;
        Inventory.Instance.OnChangeSelectedItem -= UpdateSelection;
        Inventory.Instance.OnChangeSelectedOption -= UpdateOption;
        Inventory.Instance.OnCollectItemUI -= DoInventoryItemDisplay;
        Inventory.Instance.OnDropItem -= DoRemoveItemInventory;
        Inventory.Instance.OnConsumeItem -= DoRemoveItemInventory;
    }

    private void Start()
    {
        m_Visible = false;
    }

    #region UpdateDaSeleçao
    public void UpdateSelection()
    {
        if (!Visible)
            return;

        for (int i = 0; i < m_Slots.Length; i++)
        {
            if (i == Inventory.Instance.SelectedItemIndex)
            {
                m_Slots[i].HighlightSlot(true);
                UpdateOption();
            }
            else
            {
                m_Slots[i].HighlightSlot(false);
            }
        }
    }

    public void UpdateOption()
    {
        if (!Visible)
            return;

        m_Slots[Inventory.Instance.SelectedItemIndex].Options.HighLightSelection(Inventory.Instance.SelectedOption);
    }
    #endregion

    #region Display
    public void DoInventoryDisplay()
    {
        switch (GameStateManager.Game.State)
        {
            case GameState.Inventory:
                ShowInventory();
                break;
            case GameState.World_Free:
                HideInventory();
                break;
        }
    }

    public void DoInventoryItemDisplay(Item item)
    {
        int index = Inventory.Instance.SlotByItem(item);

        m_Slots[index].SetItem(item);
    }

    public void DoRemoveItemInventory(Item item)
    {
        int itemIndex = -1;

        for (int i = 0; i < m_Slots.Length; i++)
        {
            if(m_Slots[i].ItemInSlot == item)
            {
                itemIndex = i;
                break;
            }
        }

        if (itemIndex == -1)
            return;

        m_Slots[itemIndex].SetItem(null);
    }

    private void ShowInventory()
    {
        m_Visible = true;

        m_InventoryUIElements.SetActive(true);

        UpdateSelection();
    }

    private void HideInventory()
    {
        m_Visible = false;

        m_InventoryUIElements.SetActive(false);

        UpdateSelection();
    }
    #endregion
}