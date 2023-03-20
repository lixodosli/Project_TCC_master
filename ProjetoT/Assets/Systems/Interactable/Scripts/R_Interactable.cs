using UnityEngine;

public abstract class R_Interactable : MonoBehaviour
{
    #region Propriedades
    [SerializeField] protected string m_ItemName;
    public string ItemName => m_ItemName;

    [SerializeField] protected string m_ItemID;
    public string ItemID => m_ItemID;

    [SerializeField] protected Sprite m_ItemSprite;
    public Sprite ItemIcon => m_ItemSprite;

    [SerializeField] protected bool m_CanInteract;
    public bool CanInteract => m_CanInteract;

    [SerializeField] protected bool m_CanCollect;
    public bool CanCollect => m_CanCollect;

    protected bool m_IsClose;
    public bool IsClose => m_IsClose;

    [SerializeField] protected R_ToolTip m_ToolTip;
    public R_ToolTip ToolTip => m_ToolTip;
    #endregion

    public void ChangeInteraction(bool interaction)
    {
        m_CanInteract = interaction;
    }

    public void SetIsClose(bool close)
    {
        m_IsClose = close;
    }

    public abstract void DoInteraction();

    [ContextMenu("Set Id")]
    public void SetID() => m_ItemID = System.Guid.NewGuid().ToString();
}