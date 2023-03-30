using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class R_NearbyInteractables : MonoBehaviour
{
    public static R_NearbyInteractables Instance;

    [SerializeField] private Transform m_InstigatorPoint;
    private List<R_Interactable> m_Interactables = new List<R_Interactable>();

    private void Awake()
    {
        Instance = this;
        PlayerInputManager.Instance.PlayerInput.World.Action.performed += DoInteraction;
    }

    private void OnDestroy()
    {
        PlayerInputManager.Instance.PlayerInput.World.Action.performed -= DoInteraction;
    }

    private void Update()
    {
        if (HasNearbyInteractables())
        {
            UpdateVisibleTipBox();
        }
    }

    public void DoInteraction(InputAction.CallbackContext context)
    {
        if (!HasNearbyInteractables())
            return;

        ClosestInteractables().DoInteraction();
    }

    #region ListManipulation
    public void AddInteractable(R_Interactable item)
    {
        m_Interactables.Add(item);
    }

    public void RemoveInteractable(R_Interactable item)
    {
        item.SetIsClose(false);
        m_Interactables.Remove(item);
    }

    public bool AlreadyInTheList(R_Interactable item)
    {
        for (int i = 0; i < m_Interactables.Count; i++)
        {
            if (m_Interactables[i].ItemID == item.ItemID)
            {
                return true;
            }
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        R_Interactable interactable = other.GetComponent<R_Interactable>();

        if (interactable != null)
        {
            if (interactable.CanInteract && !AlreadyInTheList(interactable))
            {
                AddInteractable(interactable);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        R_Interactable interactable = other.GetComponent<R_Interactable>();

        if (interactable != null && AlreadyInTheList(interactable))
        {
            RemoveInteractable(interactable);
        }
    }
    #endregion

    #region Utilities
    private void UpdateVisibleTipBox()
    {
        for (int i = 0; i < m_Interactables.Count; i++)
        {
            if (m_Interactables[i] == ClosestInteractables())
                m_Interactables[i].SetIsClose(true);
            else
                m_Interactables[i].SetIsClose(false);
        }
    }

    public bool HasNearbyInteractables()
    {
        return m_Interactables.Count != 0;
    }

    public R_Interactable ClosestInteractables()
    {
        int closestIndex = 0;

        for (int i = 0; i < m_Interactables.Count; i++)
        {
            var atualClosest = Vector3.Distance(m_Interactables[closestIndex].transform.position, transform.position);
            var newCheackage = Vector3.Distance(m_Interactables[i].transform.position, transform.position);

            if (newCheackage < atualClosest)
            {
                closestIndex = i;
            }
        }

        return m_Interactables[closestIndex];
    }
    #endregion
}