using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin_MediumGrowth : Useable
{
    [SerializeField] private GameObject m_Abroba;
    [SerializeField] private float m_HeightOffset;
    [SerializeField] private int m_DaysToGrowth;
    private int _DayStart;

    private void Awake()
    {
        DaySystem.Instance.OnDayEnd += Growth;
    }

    private void OnDestroy()
    {
        DaySystem.Instance.OnDayEnd -= Growth;
    }

    private void OnEnable()
    {
        _DayStart = DaySystem.Instance.DayCount;
    }

    public void Growth(int day)
    {
        if (day >= _DayStart + m_DaysToGrowth)
        {
            OnUsed(null);
        }
    }

    public override void OnUsed(R_Item item)
    {
        base.OnUsed(item);
        Vector3 position = new Vector3(transform.position.x, transform.position.y + m_HeightOffset, transform.position.z);

        GameObject broba = Instantiate(m_Abroba, position, Quaternion.identity);
        broba.GetComponent<R_Item>().SetID();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + m_HeightOffset, transform.position.z), 0.025f);
    }
}