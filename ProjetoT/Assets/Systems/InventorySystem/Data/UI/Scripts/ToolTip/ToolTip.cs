using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolTip : MonoBehaviour
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
    private ToolTipElements m_TipBox_Elements;
    private Interactable _Interaction;
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

    [ContextMenu("ToolTip")]
    public void SetupTip()
    {
        if(_Interaction == null)
        {
            _Interaction = GetComponent<Interactable>();
        }

        if(m_TipBox_Elements == null)
        {
            m_TipBox_Elements = GetComponentInChildren<ToolTipElements>(true);
        }

        _TipBox_BG = m_TipBox_Elements.GetComponentInChildren<RectTransform>();
        _TipBox_TextField = m_TipBox_Elements.GetComponentInChildren<TMP_Text>();
        _CollectBox_Elements = m_TipBox_Elements.GetComponentInChildren<CollectBox>();

        _TipBox_TextField.text = _Interaction.ItemName;

        int charCount = _TipBox_TextField.text.Length;
        float canvasWidth = TipBox_CharPadding * 2 + charCount * TipBox_CharSize;

        _TipBox_BG.sizeDelta = new Vector2(Mathf.Clamp(canvasWidth, TipBox_BG_MinSize, TipBox_BG_MaxSize), _TipBox_BG.sizeDelta.y);
    }

    public void UpdateToolTipPosition()
    {
        if (!_Interaction.IsClose)
            return;

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(_Interaction.transform.position + TipBox_PivotTargetOffset);
        _TipBox_BG.position = Vector3.Lerp(_TipBox_BG.position, screenPosition, Time.deltaTime * TipBox_SpeedUpdate);
    }

    public void UpdateToolTipVisible()
    {
        if (GameStateManager.Game.State == GameState.World_Free)
        {
            if (_Interaction.IsClose && !m_TipBox_Elements.gameObject.activeSelf)
                OpenToolTip();
            else if (!_Interaction.IsClose && m_TipBox_Elements.gameObject.activeSelf)
                CloseToolTip();
        }
        else
            CloseToolTip();
    }

    public void UpdateCollectVisible()
    {
        if (GameStateManager.Game.State == GameState.World_Free)
        {
            if (_Interaction.CanCollect && m_TipBox_Elements.gameObject.activeSelf)
            {
                _CollectBox_Elements.gameObject.SetActive(true);
            }
            else if (!_Interaction.CanCollect)
            {
                _CollectBox_Elements.gameObject.SetActive(false);
            }
        }
    }

    public void OpenToolTip()
    {
        Vector3 position = Camera.main.WorldToScreenPoint(_Interaction.transform.position + TipBox_PivotTargetOffset);

        _TipBox_BG.position = position;
        m_TipBox_Elements.gameObject.SetActive(true);
    }

    public void CloseToolTip()
    {
        m_TipBox_Elements.gameObject.SetActive(false);
    }
}