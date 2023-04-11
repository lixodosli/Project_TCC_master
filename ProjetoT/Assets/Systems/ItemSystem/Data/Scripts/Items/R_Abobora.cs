using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Abobora : R_Item
{
    [Header("Ao Consumir")]
    [SerializeField] private GameObject m_Semente;

    public override void UseItem()
    {
        R_Inventory.Instance.ConsumeItem(this);
        GameObject seed = Instantiate(m_Semente, R_Inventory.Instance.transform.position + (Vector3.up * 1f), Quaternion.identity);
        seed.GetComponent<R_Item>().SetID();
    }
}