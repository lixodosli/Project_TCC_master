using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideNearbyPoints : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        InteractablePointReferencer referencer = other.GetComponent<InteractablePointReferencer>();

        if(referencer != null)
        {
            referencer.Displayer.ShowIndication(false);
        }
    }
}