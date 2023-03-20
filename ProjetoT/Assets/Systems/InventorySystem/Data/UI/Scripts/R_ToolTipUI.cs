using UnityEngine;
using TMPro;

public class R_ToolTipUI : MonoBehaviour
{
    #region ToolTipConfigs
    [Header("ToolTip Components")]
    public RectTransform Box;
    public TMP_Text ItemNameDisplay;

    [Header("ToolTip Configs")]
    public float CharSize = 8f;
    public float CharPadding = 10f;
    public float BoxMinSize = 48f;
    public float BoxMaxSize = 160f;

    public void SetToolTip(string text)
    {
        int charCount = text.Length;
        float boxSize = (charCount * CharSize) + (CharPadding * 2);

        Vector2 newSize = new Vector2(boxSize, Box.sizeDelta.y);

        ItemNameDisplay.text = text;
        Box.sizeDelta = newSize;
    }
    #endregion
}