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
    public Sprite DefaultIcon;
    public Sprite ItemIcon;
    public ColorListener Color;
    public bool IsSelected = false;
    public ItemSettings Settings;
    public TMP_Text[] OptionsTexts;

    private void Awake()
    {
        SetupSlot();
        PlayerInputManager.Instance.PlayerInput.World.Inventory.performed += UpdateColor;
        InventoryUI.Instance.OnCallInventory += UpdateTipBoxVisible;
    }

    private void OnDestroy()
    {
        PlayerInputManager.Instance.PlayerInput.World.Inventory.performed -= UpdateColor;
        InventoryUI.Instance.OnCallInventory -= UpdateTipBoxVisible;
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
        SetItem(null);
        UpdateSlotElements();
    }

    public void SetItem(Item item)
    {
        Item = item;

        if(item != null)
            ItemIcon = item.ItemIcon;
        else
            ItemIcon = DefaultIcon;
    }

    public void UpdateOptionSelection(int index)
    {
        for (int i = 0; i < Settings.Options.Length; i++)
        {
            if (i == index)
            {
                Settings.Options[i].SetVisible(true);
                OptionsTexts[i].color = ColorPicker.Instance.GetColor(ColorKey.OffWhite);
            }
            else
            {
                Settings.Options[i].SetVisible(false);
                OptionsTexts[i].color = ColorPicker.Instance.GetColor(ColorKey.Brick);
            }
        }
    }

    public void UpdateTipBoxVisible(bool visible)
    {
        if (!visible)
            return;

        if (Item == null)
            SetTipBoxVisible(false);
        else
            SetTipBoxVisible(true);
    }

    public void UpdateSlotElements()
    {
        if(ItemIcon != null)
            ItemIconUIElement.sprite = ItemIcon;
        else
            ItemIconUIElement.sprite = DefaultIcon;

        if (Item != null)
        {
            Settings.Options[0].Setup(Item.Usage.UseText, false);
            Settings.Options[1].Setup("Largar", false);
        }
    }

    public void SetupTipBox()
    {
        if (Item != null)
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