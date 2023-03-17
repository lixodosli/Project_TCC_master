using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [SerializeField] private InventoryChannel m_Channel;
    [SerializeField] private NearbyInteractables m_Interactables;
    [SerializeField] private List<Item> m_Items = new List<Item>();
    [SerializeField] private int m_SelectedItem = 0;
    private List<Item> _LastRemovedItems = new List<Item>();

    public NearbyInteractables Interactables => m_Interactables;

    private void Awake()
    {
        Instance = this;
        PlayerInputManager.Instance.PlayerInput.World.Action.performed += PickItem;
        PlayerInputManager.Instance.PlayerInput.World.Action.performed += UseOption;
        PlayerInputManager.Instance.PlayerInput.World.Inventory.performed += SelectItem;
        PlayerInputManager.Instance.PlayerInput.World.InventoryNavigateRight.performed += NavigateInventoryRight;
        PlayerInputManager.Instance.PlayerInput.World.InventoryNavigateLeft.performed += NavigateInventoryLeft;
    }

    private void OnDestroy()
    {
        PlayerInputManager.Instance.PlayerInput.World.Action.performed -= PickItem;
        PlayerInputManager.Instance.PlayerInput.World.Action.performed -= UseOption;
        PlayerInputManager.Instance.PlayerInput.World.Inventory.performed -= SelectItem;
        PlayerInputManager.Instance.PlayerInput.World.InventoryNavigateRight.performed -= NavigateInventoryRight;
        PlayerInputManager.Instance.PlayerInput.World.InventoryNavigateLeft.performed -= NavigateInventoryLeft;
    }

    private void Start()
    {
        SetupList();
    }

    public void SelectItem(InputAction.CallbackContext context)
    {
        InventoryUI.Instance.SelectItem(m_SelectedItem);
    }

    public void SetupList()
    {
        for (int i = 0; i < 9; i++)
        {
            m_Items.Add(null);
        }
    }

    public void PickItem(InputAction.CallbackContext context)
    {
        if (InventoryUI.Instance.Visible)
            return;

        if (m_Interactables.HasNearbyInteractables() && m_Interactables.ClosestInteractables().Settings.IsCollectable && m_Interactables.ClosestInteractables().CanInteract)
        {
            int emptySlot = EmptySlot();
            Debug.Log(m_Interactables.ClosestInteractables().name);
            AddItem(m_Interactables.ClosestInteractables().Settings.Item, emptySlot);

            InventoryUI.Instance.AddItemInInventory(m_Interactables.ClosestInteractables().Settings, emptySlot);
            m_Interactables.CollectItem(transform);

            m_Interactables.RemoveInteractable(m_Interactables.ClosestInteractables());
        }
    }
     
    public void DropItem(Item item)
    {
        string d = "My item is: " + item.GameItem.name + " / " + item.ItemID.ID + "\n";
        int itemIndex = IndexByItem(item);
        d += "The finded Item is: " + item.GameItem.name + " / " + m_Items[itemIndex].ItemID.ID;
        Debug.Log(d);

        if (itemIndex < 0)
        {
            Debug.Log("item n encontrado");
            return;
        }

        InventoryUI.Instance.RemoveItemInInventory(m_SelectedItem);
        m_Items[itemIndex].GameItem.transform.localPosition = Vector3.zero;
        m_Items[itemIndex].GameItem.transform.rotation = transform.rotation;
        m_Items[itemIndex].GameItem.transform.parent = null;
        m_Items[itemIndex].GameItem.SetInteraction(true);
        m_Items[itemIndex].GameItem.gameObject.SetActive(true);
        RemoveItem(m_Items[itemIndex]);
    }

    public void UseOption(InputAction.CallbackContext context)
    {
        if (!InventoryUI.Instance.Visible)
            return;

        switch (InventoryUI.Instance.OptionSelected)
        {
            default:
            case 0:
                m_Items[m_SelectedItem].UseItem();
                break;
            case 1:
                Debug.Log(m_Items[m_SelectedItem].GameItem.name + " / " + m_Items[m_SelectedItem].ItemID.ID);
                //DropItem(m_Items[m_SelectedItem]);
                //InventoryUI.Instance.HideInventory();
                break;
        }
    }

    public void AddItem(Item item)
    {
        if (EmptySlot() < 0) // -- Não tem mais espaço vazio
            return;

        m_Items[EmptySlot()] = item; // -- Preenche o Slot
    }

    public void AddItem(Item item, int slot)
    {
        if (slot < 0) // -- Não tem mais espaço vazio
            return;

        Debug.Log(item.GameItem.name + " / " + item.ItemID.ID);
        m_Items[slot] = item; // -- Preenche o Slot
    }

    public void AddItem(InteractableItem interItem, int slot)
    {
        if (slot < 0) // -- Não tem mais espaço vazio
            return;

        Debug.Log(interItem + " / " + interItem.Settings.Item.ItemID.ID);
        m_Items[slot] = interItem.Settings.Item; // -- Preenche o Slot
    }

    public void RemoveItem(Item item)
    {
        _LastRemovedItems.Add(m_Items[IndexByItem(item)]); // -- Adiciona o item removido na lista de últimos itens removidos

        if(_LastRemovedItems.Count > 3) // -- Se a lista tiver muito grande, ele elimina os itens removidos mais antigos
            _LastRemovedItems.Remove(_LastRemovedItems[0]);

        m_Items[IndexByItem(item)] = null; // -- Remove o Item
    }

    public void RemoveItem(int index)
    {
        _LastRemovedItems.Add(ItemByIndex(index)); // -- Adiciona o item removido na lista de últimos itens removidos

        if(_LastRemovedItems.Count > 3) // -- Se a lista tiver muito grande, ele elimina os itens removidos mais antigos
            _LastRemovedItems.Remove(_LastRemovedItems[0]);

        m_Items[index] = null; // -- Remove o Item
    }

    private Item ItemByIndex(int index)
    {
        if (index > m_Items.Count - 1)
            return null;

        return m_Items[index];
    }

    private int IndexByItem(Item item)
    {
        for (int i = 0; i < m_Items.Count; i++)
        {
            if (m_Items[i].ItemID.ID == item.ItemID.ID)
                return i;
        }

        return -1;
    }

    private int EmptySlot()
    {
        for (int i = 0; i < m_Items.Count; i++)
        {
            if (m_Items[i] == null)
                return i;
        }

        return -1;
    }

    public Item SelectedItem()
    {
        return m_Items[m_SelectedItem];
    }

    public int SelectedItemIndex()
    {
        return m_SelectedItem;
    }

    public void NavigateInventoryRight(InputAction.CallbackContext context)
    {
        if (!InventoryUI.Instance.Visible)
            return;

        int desiredPosition = m_SelectedItem + 1;

        if (desiredPosition >= m_Items.Count)
            desiredPosition = 0;

        m_SelectedItem = desiredPosition;
        InventoryUI.Instance.SetOptionSelection(0);
        InventoryUI.Instance.SelectItem(m_SelectedItem);
    }

    public void NavigateInventoryLeft(InputAction.CallbackContext context)
    {
        if (!InventoryUI.Instance.Visible)
            return;

        int desiredPosition = m_SelectedItem - 1;

        if (desiredPosition < 0)
            desiredPosition = m_Items.Count - 1;

        m_SelectedItem = desiredPosition;
        InventoryUI.Instance.SetOptionSelection(0);
        InventoryUI.Instance.SelectItem(m_SelectedItem);
    }
}