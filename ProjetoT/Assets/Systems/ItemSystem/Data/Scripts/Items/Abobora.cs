using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Abobora : Item
{
    [Header("Ao Consumir")]
    [SerializeField] private GameObject m_Semente;

    public override void UseItem()
    {
        Inventory.Instance.ConsumeItem(this);
        GameObject seed = Instantiate(m_Semente, Inventory.Instance.transform.position + (Vector3.up * 1f), Quaternion.identity);
        seed.GetComponent<Item>().SetID();
    }
}