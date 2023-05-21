using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class R_ItemHolder : MonoBehaviour
{
    public R_Item Item;
    private GameObject ItemModel;

    private void OnEnable()
    {
        Setup();
    }

    private void OnDisable()
    {
        Item = null;
        transform.parent = R_ItemPooling.Instance.transform;
        Destroy(ItemModel);
        ItemModel = null;
    }

    public void Setup()
    {
        if (Item == null)
            return;

        ItemModel = Instantiate(Item.ItemModel, transform);
        ItemModel.transform.localPosition = Vector3.zero;
        ItemModel.transform.localRotation = Quaternion.identity;
    }

    private void OnValidate()
    {
        name = Item == null ? "--GameItem--" : Item.ItemName;
    }
}