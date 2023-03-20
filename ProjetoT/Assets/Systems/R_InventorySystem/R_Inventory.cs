using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class R_Inventory : MonoBehaviour
{
    public static R_Inventory Instance;

    #region ChangesEvents
    public delegate void InventoryCallback();
    public InventoryCallback OnCollectItem;
    public InventoryCallback OnDropItem;
    public InventoryCallback OnOpenCloseInventory;
    public InventoryCallback OnOpenInventory;
    public InventoryCallback OnCloseInventory;
    public InventoryCallback OnChangeSelectedItem;
    #endregion

    #region List
    [Header("Inventory List")]
    public R_Item[] Items = new R_Item[9];
    [SerializeField] private Transform m_InventoryPool;
    public Transform InventoryPool => m_InventoryPool;
    #endregion

    #region SelectionManager
    [SerializeField] private int m_SelectedItemIndex = 0;
    public int SelectedItemIndex => m_SelectedItemIndex;

    private bool _MakeInput = false;
    #endregion

    #region OpenAndClose
    private bool _InventoryVisible = false;
    #endregion

    private void Awake()
    {
        Instance = this;
        PlayerInputManager.Instance.PlayerInput.World.Inventory.performed += OpenInventory;
    }

    private void OnDestroy()
    {
        PlayerInputManager.Instance.PlayerInput.World.Inventory.performed -= OpenInventory;
    }

    private void Update()
    {
        UpdateSelectedItem();
    }

    #region SelectionManager
    public void UpdateSelectedItem()
    {
        if (GameStateManager.Game.State != GameState.Inventory)
            return;

        if (!_MakeInput)
        {
            if (Mathf.Abs(SelectionInput()) > 0.1f)
            {
                ChangeSelection(SelectionInput() > 0 ? m_SelectedItemIndex + 1 : m_SelectedItemIndex -1);
                _MakeInput = true;
            }
        }

        if (SelectionInput() == 0)
        {
            _MakeInput = false;
        }
    }

    public void ChangeSelection(int newSelection)
    {
        if (newSelection == SelectedItemIndex)
            return;

        m_SelectedItemIndex = Mathf.Clamp(newSelection, 0, 8);
        RaiseChangeSelectedItem();
    }

    public R_Item SelectedItem()
    {
        return Items[m_SelectedItemIndex];
    }

    private void RaiseChangeSelectedItem()
    {
        OnChangeSelectedItem?.Invoke();
    }

    private float SelectionInput() => PlayerInputManager.Instance.PlayerInput.World.Movement.ReadValue<Vector2>().x;
    #endregion

    #region CollectItem
    public void CollectItem(R_Item item)
    {
        int slot = EmptySlot();

        if (slot < 0)
        {
            return;
        }

        if (!TryCollect(item))
        {
            return;
        }

        Items[slot] = item;
        Items[slot].transform.position = InventoryPool.transform.position;
        Items[slot].ChangeInteraction(false);
        Items[slot].SetIsClose(false);
        Items[slot].gameObject.SetActive(false);
        RaiseCollectItem();
    }

    private bool TryCollect(R_Item item)
    {
        PoolItem(item);

        if (item.transform.parent == InventoryPool)
            return true;
        else
            return false;
    }

    private void PoolItem(R_Item item)
    {
        if (!item.CanCollect)
        {
            return;
        }

        item.transform.parent = InventoryPool;
    }
    #endregion

    #region DropItem
    public void DropItem(R_Item item)
    {
        int slot = SlotByItem(item);

        if (slot < 0)
        {
            Debug.Log("O Item <" + item.ItemName + "> não está no inventário.");
        }

        if (Items[slot] == null)
        {
            Debug.Log("Este Slot está vazio.");
            return;
        }

        item.transform.parent = null;
        RaiseDropItem();
    }
    #endregion

    #region OpenAndClose
    public void SetupInventory()
    {
        _InventoryVisible = true;
        CallForChangeInventory();
    }

    public void OpenInventory(InputAction.CallbackContext context)
    {
        CallForChangeInventory();
    }

    private void CallForChangeInventory()
    {
        bool isValidState = GameStateManager.Game.State != GameState.Cutscene && GameStateManager.Game.State != GameState.Pause;

        if (!isValidState)
        {
            return;
        }

        _InventoryVisible = !_InventoryVisible;

        if (_InventoryVisible)
            GameStateManager.Game.RaiseChangeGameState(GameState.Inventory);
        else
            GameStateManager.Game.RaiseChangeGameState(GameState.World_Free);

        OnOpenCloseInventory?.Invoke();
    }
    #endregion

    #region Utilities
    private int SlotByItem(R_Item item)
    {
        bool foundItem = false;
        int slot = -1;

        for (int i = 0; i < Items.Length; i++)
        {
            if(Items[i] == item)
            {
                foundItem = true;

                if (Items[i].ItemID == item.ItemID)
                {
                    slot = i;
                    break;
                }
            }
        }

        if(foundItem && slot < 0)
        {
            Debug.Log("O item <" + item.ItemName + "> foi encontrado, porém não o mesmo ID procurado.");
        }

        return slot;
    }

    private int EmptySlot()
    {
        int slot = -1;

        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null)
            {
                slot = i;
                break;
            }
        }

        return slot;
    }

    private void RaiseCollectItem()
    {
        OnCollectItem?.Invoke();
    }

    private void RaiseDropItem()
    {
        OnDropItem?.Invoke();
    }
    #endregion
}