using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class R_ToolTip : MonoBehaviour
{
    #region Position
    [Header("Position Configs")]
    public Vector3 TipBox_PivotTargetOffset;
    public float TipBox_SpeedUpdate;
    #endregion

    #region Display
    [Header("Display Configs")]
    public GameObject TipBox_Elements;
    public Color32 TipBox_BGColor;
    public int TipBox_CharSize;
    public int TipBox_CharPadding;
    public int TipBox_BG_MinSize;
    public int TipBox_BG_MaxSize;
    #endregion

    #region GeneralComponents
    [Header("General Component Configs")]
    public R_Interactable Interaction;
    #endregion

    #region ToolTipComponents
    [Header("Tooltip Configs")]
    public RectTransform TipBox_BG;
    public Image TipBox_BGImage;
    public TMP_Text TipBox_TextField;
    #endregion

    #region CollectComponents
    [Header("Collect Configs")]
    public GameObject Collect_Elements;
    #endregion

    private void Start()
    {
        SetupTip();
    }

    private void Update()
    {
        UpdateToolTipPosition();
        UpdateToolTipVisible();
        UpdateCollectVisible();
    }

    public void SetupTip()
    {
        TipBox_TextField.text = Interaction.ItemName;

        int charCount = TipBox_TextField.text.Length;
        float canvasWidth = TipBox_CharPadding * 2 + charCount * TipBox_CharSize;

        TipBox_BG.sizeDelta = new Vector2(Mathf.Clamp(canvasWidth, TipBox_BG_MinSize, TipBox_BG_MaxSize), TipBox_BG.sizeDelta.y);
        TipBox_BGImage.color = TipBox_BGColor;
    }

    public void UpdateToolTipPosition()
    {
        if (!Interaction.IsClose)
            return;

        Vector3 position = Camera.main.WorldToScreenPoint(Interaction.transform.position + TipBox_PivotTargetOffset);

        TipBox_BG.position = Vector3.Slerp(TipBox_BG.position, position, Time.deltaTime * 30f);
    }

    public void UpdateToolTipVisible()
    {
        if (GameStateManager.Game.State == GameState.World_Free)
        {
            if (Interaction.IsClose && !TipBox_Elements.activeSelf)
                OpenToolTip();
            else if (!Interaction.IsClose && TipBox_Elements.activeSelf)
                CloseToolTip();
        }
        else
            CloseToolTip();
    }

    public void UpdateCollectVisible()
    {
        if (GameStateManager.Game.State == GameState.World_Free)
        {
            if (Interaction.CanCollect && TipBox_Elements.activeSelf)
            {
                Collect_Elements.SetActive(true);
            }
            else if (!Interaction.CanCollect)
            {
                Collect_Elements.SetActive(false);
            }
        }
        else
            Collect_Elements.SetActive(false);
    }

    public void OpenToolTip()
    {
        Vector3 position = Camera.main.WorldToScreenPoint(Interaction.transform.position + TipBox_PivotTargetOffset);

        TipBox_BG.position = position;
        TipBox_Elements.SetActive(true);
    }

    public void CloseToolTip()
    {
        TipBox_Elements.SetActive(false);
    }
}