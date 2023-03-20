using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class R_OptionsBoxUI_Line : MonoBehaviour
{
    [Header("Display Info")]
    public string OptionText;

    [Header("Display Components")]
    public Image BoxImageDisplay;
    public TMP_Text OptionTextDisplay;
    public GameObject IndicatorImageDisplay;

    [Header("Configurations")]
    public Color32 BoxUnselectedColor;
    public Color32 BoxSelectedColor;
    public Color32 FontUnselectedColor;
    public Color32 FontSelectedColor;
    public TMP_FontAsset UnselectedFont;
    public TMP_FontAsset SelectedFont;

    private void Start()
    {
        OptionTextDisplay.text = OptionText;
    }

    public void Highlight(bool selected)
    {
        IndicatorImageDisplay.SetActive(selected);
        BoxImageDisplay.color = selected ? BoxSelectedColor : BoxUnselectedColor;
        OptionTextDisplay.color = selected ? FontSelectedColor : FontUnselectedColor;
        OptionTextDisplay.font = selected ? SelectedFont : UnselectedFont;
        RectTransform mySize = GetComponent<RectTransform>();
        mySize.localScale = selected ? Vector3.one * 1.2f : Vector3.one;
    }

    [ContextMenu("Test Highlight")]
    private void TestHighlight()
    {
        if (OptionTextDisplay.text != OptionText)
            OptionTextDisplay.text = OptionText;

        Highlight(!IndicatorImageDisplay.activeSelf);
    }
}