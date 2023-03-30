using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : Useable
{
    [SerializeField] private ParticleSystem _Particle;

    public override void OnUsed(R_Item item)
    {
        _Particle.Play();
        base.OnUsed(item);
    }
}