using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NearbyUseableSets : MonoBehaviour
{
    private List<Useable_Set> _NearbySets = new List<Useable_Set>();
    public static Useable_Set ClosestUseableSet;

    public Transform InstigatorPoint;

    private void OnTriggerEnter(Collider other)
    {
        Useable_Set set = other.GetComponent<Useable_Set>();
        if (set != null && !_NearbySets.Contains(set))
        {
            _NearbySets.Add(set);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Useable_Set set = other.GetComponent<Useable_Set>();
        if (set != null)
        {
            _NearbySets.Remove(set);
        }
    }

    private Useable_Set GetClosestSet()
    {
        if (_NearbySets.Count == 0)
            return null;

        Useable_Set closest = _NearbySets.OrderBy(i => Vector3.Distance(i.transform.position, InstigatorPoint.transform.position)).FirstOrDefault();
        return closest;
    }

    private void Update()
    {
        Useable_Set closestSet = GetClosestSet();

        ClosestUseableSet = closestSet;
    }
}