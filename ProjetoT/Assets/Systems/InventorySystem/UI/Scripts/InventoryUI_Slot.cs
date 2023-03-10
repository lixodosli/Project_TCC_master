using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class InventoryUI_Slot : MonoBehaviour
{
    public Item Item;
    public Image ItemIconUIElement;
    public TMP_Text QuantityUIElement;
    public Sprite DefaultIcon;
    public Sprite ItemIcon;
    public int Quantity;
    public ColorListener Color;
    public bool IsSelected = false;
    public ItemSettings Settings;

    private void Awake()
    {
        SetupSlot();
        PlayerInputManager.Instance.PlayerInput.World.Inventory.performed += UpdateColor;
    }

    private void OnDestroy()
    {
        PlayerInputManager.Instance.PlayerInput.World.Inventory.performed -= UpdateColor;
    }

    public void UpdateColor(InputAction.CallbackContext context)
    {
        UpdateSlotSelection();
    }

    public void UpdateSlotSelection()
    {
        if (IsSelected)
        {
            Color.SetColor(ColorKey.Yolk);

            if(Item != null)
            {
                SetupTipBox();
                SetTipBoxVisible(true);
            }
        }
        else
        {
            Color.SetColor(ColorKey.Brick);
            SetTipBoxVisible(false);
        }
    }

    public void SetupSlot()
    {
        SetItem(null, 0);
        UpdateSlotElements();
    }

    public void SetItem(Item item, int quantity)
    {
        Item = item;

        if(item != null)
            ItemIcon = item.ItemIcon;

        Quantity = quantity;
    }

    public void UpdateSlotElements()
    {
        if(ItemIcon != null)
            ItemIconUIElement.sprite = ItemIcon;
        else
            ItemIconUIElement.sprite = DefaultIcon;
        QuantityUIElement.text = Quantity > 0 ? Quantity.ToString() : "";
    }

    public void SetupTipBox()
    {
        if(Item != null)
            Settings.SetupTipBox(Item.ItemName);
    }

    public void SetTipBoxVisible(bool visible)
    {
        Settings.TipBox.gameObject.SetActive(visible);
        if (!Settings.IsCollectable)
            return;

        Settings.CollectBox.gameObject.SetActive(visible);
    }
}