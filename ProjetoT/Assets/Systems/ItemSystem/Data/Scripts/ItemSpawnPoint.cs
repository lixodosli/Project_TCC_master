using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPoint : MonoBehaviour
{
    [SerializeField] private TextAsset m_Data;
    [SerializeField] private ItemList m_Item;

    [ContextMenu("Instantiate Item")]
    public void InstantiateItem()
    {
        Debug.Log("tentando instanciar item");

        int id = int.Parse(m_Item.ToString()[1].ToString());
        string name = StringSplitter.SpaceSentence(m_Item.ToString());

        GameObject itemInstanciado = Instantiate(ItemManager.Instance.ItemPrefabByItemName(name), transform.position, Quaternion.identity);
        Item item = itemInstanciado.GetComponent<Item>();
        item.SetItemNameAndId(name, name + System.Guid.NewGuid().ToString());
    }
}