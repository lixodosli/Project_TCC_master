using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InteractionInstigator : MonoBehaviour
{
    private List<Interactable> m_NearbyInteractables = new List<Interactable>();
    [SerializeField] private InteractionChannel m_Channel;

    private void OnDisable()
    {
        m_NearbyInteractables.Clear();
    }

    private void Update()
    {
        if (HasNearbyInteractables() && Input.GetButtonDown("Submit"))
        {
            ClosestInteractables().DoInteraction();
        }
    }

    public bool HasNearbyInteractables()
    {
        return m_NearbyInteractables.Count != 0;
    }

    public Interactable ClosestInteractables()
    {
        int closestIndex = 0;

        for (int i = 0; i < m_NearbyInteractables.Count; i++)
        {
            var atualClosest = Vector3.Distance(m_NearbyInteractables[closestIndex].transform.position, transform.position);
            var newCheackage = Vector3.Distance(m_NearbyInteractables[i].transform.position, transform.position);

            if (newCheackage < atualClosest)
            {
                closestIndex = i;
            }
        }

        return m_NearbyInteractables[closestIndex];
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();

        if (interactable != null)
        {
            m_NearbyInteractables.Add(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();

        if (interactable != null)
        {
            m_NearbyInteractables.Remove(interactable);
        }
    }
}