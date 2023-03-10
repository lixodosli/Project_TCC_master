using UnityEngine;

[ExecuteInEditMode]
public class CameraFollowDegrees : MonoBehaviour
{
    [SerializeField] private float m_FollowAngle = 45f;
    [SerializeField] private float m_Distance = 10f;
    [SerializeField] private CameraFollow m_CameraSettings;

    private void Update()
    {
        Quaternion rot = Quaternion.Euler(m_FollowAngle, m_CameraSettings.Camera.rotation.y, m_CameraSettings.Camera.rotation.z);
        m_CameraSettings.Camera.LookAt(m_CameraSettings.Target);
        m_CameraSettings.Camera.rotation = rot;

        //float yPosition = Mathf.Sin(m_FollowAngle * Mathf.Deg2Rad) * m_Distance;
        //float zPosition = Mathf.Cos(m_FollowAngle * Mathf.Deg2Rad) * m_Distance;
        //Vector3 newPos = new Vector3(0f, yPosition, zPosition);

        //m_CameraSettings.Camera.transform.position = newPos;
    }
}
