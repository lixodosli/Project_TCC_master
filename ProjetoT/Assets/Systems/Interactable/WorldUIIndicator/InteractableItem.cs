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
        Settings.Item.GameItem = this;
        GenerateItemID();
        string d = Settings.Item.GameItem.name + "\n" + Settings.Item.ItemID.ID;
        Debug.Log(d);
    }

    private void Update()
    {
        SetupTipPosition();
    }

    public void GenerateItemID()
    {
        m_Settings.Item.GenerateID();
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

        m_Settings.TipBox.position = Vector3.Slerp(m_Settings.TipBox.position, position, Time.deltaTime * 20f);
    }

    public void SetTipBoxVisible(bool visible)
    {
        m_Settings.TipBox.gameObject.SetActive(visible);
        if (!m_Settings.IsCollectable)
            return;

        m_Settings.CollectBox.gameObject.SetActive(visible);
    }
}