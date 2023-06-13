using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DonationBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Item>(out var itemCollided))
        {
            DonationsManager.Instance.AddDonation(DonationsManager.Instance.PointsConfigs.Points(itemCollided));
            Messenger.Broadcast(NearbyInteractables.InteractableName, itemCollided.ItemName);
            itemCollided.transform.position = new Vector3(9999f, 0, 9999f);
            itemCollided.gameObject.SetActive(false);
            itemCollided.GetComponent<Rigidbody>().useGravity = false;
            //Destroy(itemCollided.gameObject, 1f);
        }
    }
}