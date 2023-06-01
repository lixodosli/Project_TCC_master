using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

public class R_PlayerItemInstigator : MonoBehaviour
{
    public R_Inventory Inventory;

    private List<R_Item> ItemsClose = new List<R_Item>();
    public static R_Item ClosestItem;

    private void Awake()
    {
        PlayerInputManager.Instance.PlayerInput.World.Action.performed += CollectItem;
    }

    private void OnDestroy()
    {
        PlayerInputManager.Instance.PlayerInput.World.Action.performed -= CollectItem;
    }

    private void Update()
    {
        if (ItemsClose.Count > 0)
            ClosestItem = ItemsClose.OrderBy(i => Vector3.Distance(i.transform.position, transform.position)).FirstOrDefault();
    }

    public void CollectItem(InputAction.CallbackContext context)
    {
        if (GameStateManager.Game.State != GameState.World_Free)
            return;

        if (ClosestItem == null)
            return;

        Inventory.Add(ClosestItem.Item);
        ClosestItem.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<R_Item>(out var item))
        {
            ItemsClose.Add(item);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<R_Item>(out var item))
        {
            ItemsClose.Remove(item);
        }
    }
}