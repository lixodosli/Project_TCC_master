using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class R_Item : MonoBehaviour
{
    public R_ItemConfigs Item;
    private R_ItemConfigs LateItem;

    [SerializeField] private MeshRenderer _Renderer;
    [SerializeField] private MeshFilter _Filter;
    [SerializeField] private MeshCollider _Collider;

    private MeshRenderer _ItemModelRenderer;
    private MeshFilter _ItemModelFilter;

    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        if (Item != LateItem)
            Setup();
    }

    private void LateUpdate()
    {
        LateItem = Item;
    }

    public void Setup()
    {
        if (Item == null)
            return;

        _ItemModelRenderer = Item.ItemModel.GetComponent<MeshRenderer>();
        _ItemModelFilter = Item.ItemModel.GetComponent<MeshFilter>();

        _Renderer.materials = _ItemModelRenderer.sharedMaterials;
        _Filter.mesh = _ItemModelFilter.sharedMesh;
        _Collider.sharedMesh = _ItemModelFilter.sharedMesh;
    }

    private void OnValidate()
    {
        name = Item == null ? "--GameItem--" : Item.ItemName;
    }
}