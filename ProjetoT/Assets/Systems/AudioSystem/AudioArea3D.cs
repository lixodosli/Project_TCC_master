using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AudioArea3D : MonoBehaviour
{
    [SerializeField] private AudioType m_Type;
    [SerializeField] private AudioClip m_AudioClip;
    [SerializeField] private AudioConfigs m_Configs;

    private void OnTriggerEnter(Collider other)
    {
        AudioInstigator instigator = other.GetComponent<AudioInstigator>();

        if(instigator != null)
        {
            AudioManager.Instance.Play(m_AudioClip, m_Type, m_Configs);
        }
    }
}