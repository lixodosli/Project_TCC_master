using UnityEngine;
  
[System.Serializable]
public abstract class Interactable : MonoBehaviour, IInteractable
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

    protected ToolTip m_ToolTip;
    public ToolTip ToolTip => m_ToolTip;

    [SerializeField] protected AudioClip m_OnInteractSFX;
    public AudioClip OnInteractSFX => m_OnInteractSFX;
    #endregion

    private void OnDisable()
    {
        Messenger.Broadcast(NearbyInteractables.InteractableName, ItemName);
    }

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
    public void SetID() => m_ItemID = ItemName + "." + System.Guid.NewGuid().ToString();
}