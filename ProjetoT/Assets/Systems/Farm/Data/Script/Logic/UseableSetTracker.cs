using UnityEngine;
using UnityEngine.UI;

public class UseableSetTracker : MonoBehaviour
{
    public Image image;
    public Vector3 offset;
    public float speed = 5f;

    private Transform target;

    private void UpdateTarget()
    {
        target = NearbyUseableSets.ClosestUseableSet != null ? NearbyUseableSets.ClosestUseableSet.transform : null;
        image.enabled = target != null;
    }

    private void LateUpdate()
    {
        UpdateTarget();

        if (target != null)
        {
            Vector3 targetPos = Camera.main.WorldToScreenPoint(target.position + offset);
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
        }
    }
}