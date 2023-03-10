using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableItem : MonoBehaviour
{
    public bool CanInteract { get; private set; }
    [SerializeField] private string m_ItemName;
    [SerializeField] private Vector3 m_Offset = new Vector3();
    [SerializeField] private ItemSettings m_Settings;
    public ItemSettings Settings => m_Settings;

    private void Start()
    {
        SetupTipBox();
        SetTipBoxVisible(false);
        SetInteraction(true);
    }

    private void Update()
    {
        SetupTipPosition();
    }

    public void SetInteraction(bool interact)
    {
        CanInteract = interact;
    }

    public bool Visible()
    {
        return m_Settings.TipBox.gameObject.activeSelf;
    }

    public void SetupTipBox()
    {
        m_Settings.SetupTipBox(m_ItemName);
    }

    public void SetupTipPosition()
    {
        Vector3 position = Camera.main.WorldToScreenPoint(transform.position + m_Offset);

        m_Settings.TipBox.position = position;
    }

    public void SetTipBoxVisible(bool visible)
    {
        m_Settings.TipBox.gameObject.SetActive(visible);
        if (!m_Settings.IsCollectable)
            return;

        m_Settings.CollectBox.gameObject.SetActive(visible);
    }
}