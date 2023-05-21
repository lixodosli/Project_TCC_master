using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class R_ItemPooling : MonoBehaviour
{
    public static R_ItemPooling Instance;

    public GameObject HolderPrefab;
    public List<R_ItemHolder> Holders = new List<R_ItemHolder>();

    private void Awake()
    {
        Instance = this;
        Setup();
    }

    public void UpdateDisable(R_ItemHolder holder)
    {
        holder.Item = null;
        holder.transform.parent = transform;
    }

    public R_ItemHolder GetItem(R_Item item)
    {
        if(Holders.Count < 1)
        {
            GameObject go = Instantiate(HolderPrefab, transform);
            Holders.Add(go.GetComponent<R_ItemHolder>());
        }

        R_ItemHolder popedItem = Holders[0];
        popedItem.Item = item;
        popedItem.Setup();

        return popedItem;
    }

    public void Setup()
    {
        R_ItemHolder[] list = FindObjectsOfType<R_ItemHolder>();

        foreach (R_ItemHolder item in list)
        {
            Holders.Add(item);
        }
    }
}