using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPoint : MonoBehaviour
{
    [SerializeField] private TextAsset m_Data;
    [SerializeField] private ItemList m_Item;

    public static void InstantiateItem(Item item, Transform spawnSpot)
    {
        Debug.Log("tentando instanciar item");

        int id = int.Parse(item.ToString()[1].ToString());
        string name = StringSplitter.SpaceSentence(item.ToString());

        GameObject itemInstanciado = Instantiate(ItemManager.Instance.ItemPrefabByItemName(name), spawnSpot.position, Quaternion.identity);
        Item instanciatedItem = itemInstanciado.GetComponent<Item>();
        instanciatedItem.SetItemNameAndId(name, name + System.Guid.NewGuid().ToString());
    }

    public static void InstantiateItemByList(ItemList item, Transform spawnSpot)
    {
        Debug.Log("tentando instanciar item");

        int id = int.Parse(item.ToString()[1].ToString());
        string name = StringSplitter.SpaceSentence(item.ToString());

        GameObject itemInstanciado = Instantiate(ItemManager.Instance.ItemPrefabByItemName(name), spawnSpot.position, Quaternion.identity);
        Item instanciatedItem = itemInstanciado.GetComponent<Item>();
        instanciatedItem.SetItemNameAndId(name, name + System.Guid.NewGuid().ToString());
    }

    public static Item TryInstantiateItem(ItemList item, Transform spawnSpot)
    {
        Debug.Log("tentando instanciar item");

        int id = int.Parse(item.ToString()[1].ToString());
        string name = StringSplitter.SpaceSentence(item.ToString());

        GameObject itemInstanciado = Instantiate(ItemManager.Instance.ItemPrefabByItemName(name), spawnSpot.position, Quaternion.identity);
        Item instanciatedItem = itemInstanciado.GetComponent<Item>();
        instanciatedItem.SetItemNameAndId(name, name + System.Guid.NewGuid().ToString());

        return instanciatedItem;
    }

    public static GameObject InstItem(ItemList item, Transform spawnSpot)
    {
        Debug.Log("tentando instanciar item");

        //int id = int.Parse(item.ToString()[1].ToString());
        int id = (int)item;
        string name = StringSplitter.SpaceSentence(item.ToString());

        GameObject itemInstanciado = Instantiate(ItemManager.Instance.ItemPrefabByItemName(name), spawnSpot.position, Quaternion.identity);
        Item instanciatedItem = itemInstanciado.GetComponent<Item>();
        instanciatedItem.SetItemNameAndId(name, name + System.Guid.NewGuid().ToString());

        return itemInstanciado;
    }

    public static Item GetItemPrefab(ItemList item)
    {
        Debug.Log("Trying to get item prefab");

        string name = StringSplitter.SpaceSentence(item.ToString());

        GameObject itemPrefab = ItemManager.Instance.ItemPrefabByItemName(name);
        Item itemComponent = itemPrefab.GetComponent<Item>();

        return itemComponent;
    }
}