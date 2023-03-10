using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbyInteractables : MonoBehaviour
{
    [SerializeField] private Transform m_InstigatorPoint;
    [SerializeField] private float m_MinimunDistance;
    private List<InteractableItem> m_Interactables = new List<InteractableItem>();

    private void Update()
    {
        if (HasNearbyInteractables())
        {
            if (!ClosestInteractables().Visible())
            {
                AllInteractableVisible(false);

                ClosestInteractables().SetTipBoxVisible(true);
            }
        }
    }

    private void AllInteractableVisible(bool condition)
    {
        foreach (var item in m_Interactables)
        {
            item.SetTipBoxVisible(condition);
        }
    }

    public bool HasNearbyInteractables()
    {
        return m_Interactables.Count != 0;
    }

    public bool IsInDistanceToInteract(InteractableItem displayer)
    {
        return Mathf.Abs(Vector3.Distance(m_InstigatorPoint.position, ClosestInteractables().transform.position)) <= m_MinimunDistance;
    }

    public void RemoveInteractable(InteractableItem item)
    {
        m_Interactables.Remove(item);
    }

    public void CollectItem(Transform player)
    {
        ClosestInteractables().transform.parent = player;
        ClosestInteractables().transform.localPosition = Vector3.zero;
        ClosestInteractables().SetInteraction(false);
        ClosestInteractables().gameObject.SetActive(false);
    }

    public InteractableItem ClosestInteractables()
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

    public InteractableItem ClosestInteractablesCollectable()
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

    private void OnTriggerEnter(Collider other)
    {
        InteractableItem interactable = other.GetComponent<InteractableItem>();

        if (interactable != null)
        {
            if(interactable.CanInteract)
                m_Interactables.Add(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InteractableItem interactable = other.GetComponent<InteractableItem>();

        if (interactable != null)
        {
            interactable.SetTipBoxVisible(false);
            m_Interactables.Remove(interactable);
        }
    }
}