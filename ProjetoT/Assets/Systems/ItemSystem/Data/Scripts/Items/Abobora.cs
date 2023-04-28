using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Abobora : Item
{
    [Header("Ao Consumir")]
    [SerializeField] private ItemList m_ItemToDrop;
    [SerializeField] private int m_DropQuantity;

    public override void UseItem()
    {
        Inventory.Instance.ConsumeItem(this);

        for (int i = 0; i < m_DropQuantity; i++)
        {
            ItemSpawnPoint.InstItem(m_ItemToDrop, transform);
        }
    }
}