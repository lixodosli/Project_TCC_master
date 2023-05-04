using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSunPosition : MonoBehaviour
{
    [SerializeField] private Transform m_Sun;
    public float RotationSpeed;
    private float _Counter;
    private float _PercentageComplete;
    private float _StartRot;
    private float _EndRot;
    private bool _Updating;
    private Quaternion _StartRotation;
    private Vector3 _TargetRotation;

    private void Awake()
    {
        StartUpdateRotation(0);
        Messenger.AddListener<int>(TimeManager.AdvanceTimeString, StartUpdateRotation);
    }

    private void Update()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        if (_Updating)
        {
            _Counter += Time.deltaTime;
            _PercentageComplete = _Counter / RotationSpeed;

            m_Sun.rotation = Quaternion.Lerp(_StartRotation, Quaternion.Euler(_TargetRotation), _PercentageComplete);

            if(_PercentageComplete >= RotationSpeed)
            {
                _Updating = false;
            }
        }
    }

    private void StartUpdateRotation(int hour)
    {
        _Counter = 0;
        _Updating = true;
        _EndRot = (360 / 24) * TimeManager.CurrentHour - 90;
        _StartRotation = m_Sun.rotation;
        _TargetRotation = new Vector3(_EndRot, m_Sun.rotation.y, m_Sun.rotation.z);
    }
}