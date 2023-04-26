using System.Collections.Generic;
using UnityEngine;

public class NearbyUseableSets : MonoBehaviour
{
    private List<Useable_Set> m_NearbySets = new List<Useable_Set>();
    public static Useable_Set ClosestUseableSet;

    public Transform InstigatorPoint;

    private void OnTriggerEnter(Collider other)
    {
        Useable_Set set = other.GetComponent<Useable_Set>();
        if (set != null && !m_NearbySets.Contains(set))
        {
            m_NearbySets.Add(set);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Useable_Set set = other.GetComponent<Useable_Set>();
        if (set != null)
        {
            m_NearbySets.Remove(set);
        }
    }

    private Useable_Set GetClosestSet()
    {
        float closestDistance = float.MaxValue;
        Useable_Set closestSet = null;

        foreach (Useable_Set set in m_NearbySets)
        {
            float distance = Vector3.Distance(InstigatorPoint.position, set.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestSet = set;
            }
        }

        return closestSet;
    }

    private void Update()
    {
        Useable_Set closestSet = GetClosestSet();

        ClosestUseableSet = closestSet;
    }
}