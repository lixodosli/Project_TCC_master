using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class R_Item : MonoBehaviour
{
    #region Propriedades
    [SerializeField] protected string m_ItemName;
    public string ItemName => m_ItemName;

    [SerializeField] protected string m_ItemID;
    public string ItemID => m_ItemID;

    [SerializeField] protected Sprite m_ItemSprite;
    public Sprite ItemIcon => m_ItemSprite;
    #endregion

    #region Caracteristicas
    [SerializeField] protected bool m_CanCollect;
    public bool CanCollect => m_CanCollect;
    #endregion

    public abstract void UseItem();
}