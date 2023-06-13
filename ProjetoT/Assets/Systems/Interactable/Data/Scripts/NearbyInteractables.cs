using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NearbyInteractables : MonoBehaviour
{
    private List<Interactable> _NearbyInteractables = new List<Interactable>();
    public static Interactable ClosestInteractable;
    public static string InteractableName = "Interactable";

    public Transform InstigatorPoint;

    private void Start()
    {
        Messenger.AddListener<string>(InteractableName, UpdateList);
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable inter = other.GetComponent<Interactable>();
        if(inter != null && !_NearbyInteractables.Contains(inter))
        {
            _NearbyInteractables.Add(inter);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable inter = other.GetComponent<Interactable>();
        if(inter != null)
        {
            inter.SetIsClose(false);
            _NearbyInteractables.Remove(inter);
        }
    }

    private Interactable GetClosestInteractable()
    {
        if (_NearbyInteractables.Count == 0)
            return null;

        Interactable closest = _NearbyInteractables.OrderBy(i => Vector3.Distance(i.transform.position, InstigatorPoint.position)).FirstOrDefault();
        return closest;
    }

    private void UpdateList(string itemName)
    {
        Interactable interactable = _NearbyInteractables.Find(i => i.ItemName == itemName);

        if (interactable != null && !interactable.gameObject.activeSelf)
        {
            interactable.SetIsClose(false);
            _NearbyInteractables.Remove(interactable);
        }
    }

    private void UpdateTipBoxVisible()
    {
        if (_NearbyInteractables.Count == 0)
            return;

        _NearbyInteractables.ForEach(i => i.SetIsClose(i == ClosestInteractable));
    }

    private void Update()
    {
        Interactable closest = GetClosestInteractable();

        ClosestInteractable = closest;
        UpdateTipBoxVisible();
    }
}