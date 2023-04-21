using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerraLimpa : Useable
{
    [SerializeField] private int m_TimeToExecute;
    [SerializeField] private ParticleSystem m_Particle;

    public override void OnUsed(Item item)
    {
        Messenger.Broadcast(TimeManager.AdvanceTimeString, m_TimeToExecute);
        m_Particle.Play();
        base.OnUsed(item);
    }
}