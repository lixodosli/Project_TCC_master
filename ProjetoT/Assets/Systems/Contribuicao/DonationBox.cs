using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DonationBox : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Item itemCollided = collision.gameObject.GetComponent<Item>();

        if(itemCollided != null)
        {
            DonationsManager.Instance.AddDonation(DonationsManager.Instance.PointsConfigs.Points(itemCollided));
            Destroy(itemCollided);
        }
    }
}