using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Date_UI : MonoBehaviour
{
    [SerializeField] private Transform m_Minutes;
    [SerializeField] private Transform m_Hours;
    [SerializeField] private Transform[] m_Rotations;
    [SerializeField] private float m_TimeToRotate = 2f;

    private bool _IsRotating;
    private float _Count;
    private float _Percentage;
    private Quaternion _StartRot;
    private Quaternion _EndRot;

    private void Awake()
    {
        DateSystem.Instance.OnUpdateMinute += UpdateMinutePointer;
    }

    private void OnDestroy()
    {
        DateSystem.Instance.OnUpdateMinute -= UpdateMinutePointer;
    }

    private void Update()
    {
        if (_IsRotating)
        {
            _Count += Time.deltaTime;
            _Percentage = _Count / m_TimeToRotate;

            if(_Count > m_TimeToRotate)
            {
                m_Minutes.rotation = _EndRot;
                _IsRotating = false;
            }
            else
            {
                m_Minutes.rotation = Quaternion.Lerp(_StartRot, _EndRot, _Percentage);
            }
        }
    }

    public void UpdateMinutePointer(int minute)
    {
        _StartRot = m_Minutes.rotation;
        _EndRot = m_Rotations[minute].rotation;
        _IsRotating = true;
        _Count = 0;
    }
}