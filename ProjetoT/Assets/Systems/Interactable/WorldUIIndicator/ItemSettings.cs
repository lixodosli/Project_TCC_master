using UnityEngine;
using TMPro;
using System;

[System.Serializable]
public class ItemSettings
{
    [Header("Components")]
    public RectTransform TipBox;
    public TMP_Text TipText;

    [Header("Char Spacing")]
    public float CharSize;
    public float CharPadding;

    [Header("Tip Box Sizing")]
    public float TipBoxMinSize;
    public float TipBoxMaxSize;

    [Header("Collectable Configs")]
    public bool IsCollectable;
    public RectTransform CollectBox;
    public TMP_Text CollectText;
    public Item Item;
    public string ItemID;

    [Header("Options Configs")]
    public InventoryItemOption[] Options;

    public void SetupTipBox(string text)
    {
        int charCount = text.Length;
        float canvasWidth = CharPadding * 2 + charCount * CharSize;

        TipBox.sizeDelta = new Vector2(Mathf.Clamp(canvasWidth, TipBoxMinSize, TipBoxMaxSize), TipBox.sizeDelta.y);
        TipText.text = text;

        if (!IsCollectable)
            return;

        CollectText.text = "E";
        CollectBox.gameObject.SetActive(true);
    }

    public static explicit operator ItemSettings(UnityEngine.Object v)
    {
        throw new NotImplementedException();
    }
}