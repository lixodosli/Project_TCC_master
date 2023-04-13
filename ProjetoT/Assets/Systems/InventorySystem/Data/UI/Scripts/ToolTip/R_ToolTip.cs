using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class R_ToolTip : MonoBehaviour
{
    #region Position
    [Header("Position Configs")]
    public Vector3 TipBox_PivotTargetOffset;
    public float TipBox_SpeedUpdate = 30;
    #endregion

    #region Display
    [Header("Display Configs")]
    public int TipBox_CharSize = 8;
    public int TipBox_CharPadding = 10;
    public int TipBox_BG_MinSize = 48;
    public int TipBox_BG_MaxSize = 160;
    #endregion

    #region GeneralComponents
    [Header("General Component Configs")]
    public R_Interactable Interaction;
    public GameObject TipBox_Elements;
    #endregion

    #region ToolTipComponents
    private RectTransform _TipBox_BG;
    private TMP_Text _TipBox_TextField;
    private CollectBox _CollectBox_Elements;
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
        if(TipBox_Elements == null)
        {
            Debug.Log("A interacao <" + Interaction.name + "> esta sem tipbox e precisa ser atualizada.");
            return;
        }

        _TipBox_BG = TipBox_Elements.GetComponentInChildren<RectTransform>();
        _TipBox_TextField = TipBox_Elements.GetComponentInChildren<TMP_Text>();
        _CollectBox_Elements = TipBox_Elements.GetComponentInChildren<CollectBox>();

        _TipBox_TextField.text = Interaction.ItemName;

        int charCount = _TipBox_TextField.text.Length;
        float canvasWidth = TipBox_CharPadding * 2 + charCount * TipBox_CharSize;

        _TipBox_BG.sizeDelta = new Vector2(Mathf.Clamp(canvasWidth, TipBox_BG_MinSize, TipBox_BG_MaxSize), _TipBox_BG.sizeDelta.y);
    }

    public void UpdateToolTipPosition()
    {
        if (!Interaction.IsClose)
            return;

        Vector3 position = Camera.main.WorldToScreenPoint(Interaction.transform.position + TipBox_PivotTargetOffset);

        _TipBox_BG.position = Vector3.Slerp(_TipBox_BG.position, position, Time.deltaTime * TipBox_SpeedUpdate);
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
                _CollectBox_Elements.gameObject.SetActive(true);
            }
            else if (!Interaction.CanCollect)
            {
                _CollectBox_Elements.gameObject.SetActive(false);
            }
        }
        //else
        //    _CollectBox_Elements.gameObject.SetActive(false);
    }

    public void OpenToolTip()
    {
        Vector3 position = Camera.main.WorldToScreenPoint(Interaction.transform.position + TipBox_PivotTargetOffset);

        _TipBox_BG.position = position;
        TipBox_Elements.SetActive(true);
    }

    public void CloseToolTip()
    {
        TipBox_Elements.SetActive(false);
    }
}