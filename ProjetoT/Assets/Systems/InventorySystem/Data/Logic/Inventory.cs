using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    #region ChangesEvents
    public delegate void InventoryCallback();
    public InventoryCallback OnCollectItem;
    public InventoryCallback OnOpenCloseInventory;
    public InventoryCallback OnChangeSelectedItem;
    public InventoryCallback OnChangeSelectedOption;

    public delegate void ItemInInventoryCallback(Item item);
    public ItemInInventoryCallback OnCollectItemUI;
    public ItemInInventoryCallback OnUseItem;
    public ItemInInventoryCallback OnDropItem;
    public ItemInInventoryCallback OnConsumeItem;
    #endregion

    #region List
    [Header("Inventory List")]
    public Item[] Items = new Item[9];
    [SerializeField] private Transform m_InventoryPool;
    public Transform InventoryPool => m_InventoryPool;
    #endregion

    #region SelectionManager
    [SerializeField] private int m_SelectedItemIndex = 0;
    public int SelectedItemIndex => m_SelectedItemIndex;

    private bool _MakeInputX = false;
    private bool _MakeInputY = false;

    [SerializeField] private int _SelectedOption = 0;
    public int SelectedOption => _SelectedOption;
    #endregion

    #region OpenAndClose
    private bool _InventoryVisible = false;
    #endregion

    #region DropConfigs
    [Header("Drop Item Configs")]
    [SerializeField] private Vector3 m_DropOffset;
    private bool _IsDropping;
    #endregion

    private void Awake()
    {
        Instance = this;
        PlayerInputManager.Instance.PlayerInput.World.Inventory.performed += OpenInventory;
        PlayerInputManager.Instance.PlayerInput.World.Action.performed += SelectOption;
        OnDropItem += DropItem;
        OnUseItem += UseItem;
    }

    private void OnDestroy()
    {
        PlayerInputManager.Instance.PlayerInput.World.Inventory.performed -= OpenInventory;
        PlayerInputManager.Instance.PlayerInput.World.Action.performed -= SelectOption;
        OnDropItem -= DropItem;
        OnUseItem -= UseItem;
    }

    private void Update()
    {
        UpdateSelectedItemInput();
        UpdateSelectionInput();
    }

    #region SelectionManager
    public void UpdateSelectedItemInput()
    {
        if (GameStateManager.Game.State != GameState.Inventory)
            return;

        if (!_MakeInputX)
        {
            if (Mathf.Abs(SelectionInput()) > 0.1f)
            {
                ChangeSelection(SelectionInput() > 0 ? m_SelectedItemIndex + 1 : m_SelectedItemIndex -1);
                _MakeInputX = true;
            }
        }

        if (SelectionInput() == 0)
        {
            _MakeInputX = false;
        }
    }

    public void ChangeSelection(int newSelection)
    {
        if (newSelection == SelectedItemIndex)
            return;

        m_SelectedItemIndex = Mathf.Clamp(newSelection, 0, 8);
        RaiseChangeSelectedItem();
    }

    public Item SelectedItem()
    {
        return Items[m_SelectedItemIndex];
    }

    private void RaiseChangeSelectedItem()
    {
        OnChangeSelectedItem?.Invoke();
    }

    private float SelectionInput() => PlayerInputManager.Instance.PlayerInput.World.Movement.ReadValue<Vector2>().x;
    #endregion

    #region OptionsInfo
    public void ChangeSelectionY(int newSelection)
    {
        if (newSelection == _SelectedOption)
            return;

        _SelectedOption = Mathf.Clamp(newSelection, 0, 1);
        RaiseSelectedOption();
    }

    public void UpdateSelectionInput()
    {
        if (GameStateManager.Game.State != GameState.Inventory || Items[SelectedItemIndex] == null)
            return;

        if (!_MakeInputY)
        {
            if (Mathf.Abs(SelectionInputY()) > 0.1f)
            {
                ChangeSelectionY(SelectionInputY() > 0 ? _SelectedOption - 1 : _SelectedOption + 1);
                _MakeInputY = true;
            }
        }

        if (SelectionInputY() == 0)
        {
            _MakeInputY = false;
        }
    }

    public void SelectOption(InputAction.CallbackContext context)
    {
        if (GameStateManager.Game.State != GameState.Inventory || Items[SelectedItemIndex] == null)
            return;

        switch (_SelectedOption)
        {
            default:
                return;
            case 0:
                OnUseItem?.Invoke(Items[SelectedItemIndex]);
                break;
            case 1:
                OnDropItem?.Invoke(Items[SelectedItemIndex]);
                break;
        }
    }

    public void RaiseSelectedOption()
    {
        OnChangeSelectedOption?.Invoke();
    }

    private float SelectionInputY() => PlayerInputManager.Instance.PlayerInput.World.Movement.ReadValue<Vector2>().y;
    #endregion

    #region CollectItem
    public void CollectItem(Item item)
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
        if (Items[slot].ItemID == null)
            Items[slot].SetID();
        Items[slot].transform.position = InventoryPool.transform.position;
        Items[slot].ChangeInteraction(false);
        Items[slot].gameObject.SetActive(false);
        NearbyInteractables.Instance.RemoveInteractable(item);
        RaiseCollectItem(Items[slot]);
    }

    private bool TryCollect(Item item)
    {
        PoolItem(item);

        if (item.transform.parent == InventoryPool)
            return true;
        else
            return false;
    }

    private void PoolItem(Item item)
    {
        if (!item.CanCollect)
        {
            return;
        }

        item.transform.parent = InventoryPool;
    }
    #endregion

    #region DropItem
    public void DropItem(Item item)
    {
        int slot = SlotByItem(item);

        if (slot < 0)
        {
            Debug.Log("O Item <" + item.ItemName + "> não está no inventário.");
            return;
        }

        if (Items[slot] == null)
        {
            Debug.Log("Este Slot está vazio.");
            return;
        }

        Items[slot] = null;
        item.transform.parent = null;
        item.transform.position = transform.position + transform.forward + m_DropOffset;
        item.transform.rotation = Quaternion.identity;
        item.ChangeInteraction(true);
        item.gameObject.SetActive(true);
        CallForChangeInventory();
    }
    #endregion

    #region UseItem
    public void UseItem(Item item)
    {
        item.UseItem();
        CallForChangeInventory();
    }

    public void ConsumeItem(Item item)
    {
        int slot = SlotByItem(item);

        if (slot < 0)
        {
            Debug.Log("O Item <" + item.ItemName + "> não está no inventário.");
            return;
        }

        if (Items[slot] == null)
        {
            Debug.Log("Este Slot está vazio.");
            return;
        }

        Items[slot] = null;
        OnConsumeItem?.Invoke(item);
        Destroy(item.gameObject);
        //item.transform.parent = null;
        //item.transform.position = transform.position + transform.forward + m_DropOffset;
        //item.transform.rotation = Quaternion.identity;
        //item.ChangeInteraction(true);
        //item.gameObject.SetActive(true);
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
    public int SlotByItem(Item item)
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

    private void RaiseCollectItem(Item item)
    {
        OnCollectItem?.Invoke();
        OnCollectItemUI?.Invoke(item);
    }
    #endregion
}