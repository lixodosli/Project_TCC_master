using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : Useable
{
    [SerializeField] private int m_TimeToExecute;
    [SerializeField] private ParticleSystem m_Particle;

    public override void OnUsed(R_Item item)
    {
        DateSystem.Instance.RaiseUpdateDate(m_TimeToExecute);
        m_Particle.Play();
        base.OnUsed(item);
    }
}