using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBezier : MonoBehaviour
{
    [SerializeField] private Transform m_PointA;
    [SerializeField] private Transform m_PointB;
    [SerializeField] private Transform m_PointC;
    [SerializeField] private Transform m_PointD;

    public Vector3 MiddlePoint(float t)
    {
        Vector3 ABLerp = Vector3.Lerp(m_PointA.position, m_PointB.position, t);
        Vector3 BCLerp = Vector3.Lerp(m_PointB.position, m_PointC.position, t);
        Vector3 CDLerp = Vector3.Lerp(m_PointC.position, m_PointD.position, t);
        Vector3 ABCLerp = Vector3.Lerp(ABLerp, BCLerp, t);
        Vector3 BCDLerp = Vector3.Lerp(BCLerp, CDLerp, t);

        return Vector3.Lerp(ABCLerp, BCDLerp, t);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(m_PointA.position, m_PointB.position);
        Gizmos.DrawLine(m_PointC.position, m_PointD.position);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(m_PointA.position, m_PointD.position);
    }
}
