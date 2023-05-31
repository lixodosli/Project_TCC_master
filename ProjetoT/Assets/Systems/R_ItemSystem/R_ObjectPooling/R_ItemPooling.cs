using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class R_ItemPooling : MonoBehaviour
{
    public static R_ItemPooling Instance;

    public GameObject HolderPrefab;
    public List<R_Item> Holders = new List<R_Item>();

    private void Awake()
    {
        Instance = this;
        Setup();
    }

    public void UpdateDisable(R_Item holder)
    {
        holder.Item = null;
        holder.transform.parent = transform;
    }

    public R_Item GetItem(R_ItemConfigs item)
    {
        if(Holders.Count < 1)
        {
            GameObject go = Instantiate(HolderPrefab, transform);
            Holders.Add(go.GetComponent<R_Item>());
        }

        R_Item popedItem = Holders[0];
        popedItem.Item = item;
        popedItem.Setup();

        return popedItem;
    }

    public void Setup()
    {
        R_Item[] list = FindObjectsOfType<R_Item>();

        foreach (R_Item item in list)
        {
            Holders.Add(item);
        }
    }
}