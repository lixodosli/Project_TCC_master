using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class R_InventoryUI : MonoBehaviour
{
    #region Singleton
    public static R_InventoryUI Instance;
    #endregion

    #region Elementos
    [SerializeField] private GameObject m_InventoryUIElements;
    [SerializeField] private R_InventoryUI_Slot[] m_Slots = new R_InventoryUI_Slot[9];
    #endregion

    #region Caracteristicas
    [SerializeField] private bool m_Visible;
    public bool Visible => m_Visible;
    #endregion

    private void Awake()
    {
        Instance = this;
        R_Inventory.Instance.OnOpenCloseInventory += DoInventoryDisplay;
        R_Inventory.Instance.OnChangeSelectedItem += UpdateSelection;
        R_Inventory.Instance.OnCollectItemUI += DoInventoryItemDisplay;
    }

    private void OnDestroy()
    {
        R_Inventory.Instance.OnOpenCloseInventory -= DoInventoryDisplay;
        R_Inventory.Instance.OnChangeSelectedItem -= UpdateSelection;
        R_Inventory.Instance.OnCollectItemUI -= DoInventoryItemDisplay;

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
            if (i == R_Inventory.Instance.SelectedItemIndex)
            {
                m_Slots[i].Select(true);
            }
            else
            {
                m_Slots[i].Select(false);
            }
        }
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

    public void DoInventoryItemDisplay(R_Item item)
    {
        int index = R_Inventory.Instance.SlotByItem(item);

        m_Slots[index].SetItem(item);
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