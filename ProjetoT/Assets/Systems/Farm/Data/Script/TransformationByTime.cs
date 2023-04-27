using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TransformationByTime : MonoBehaviour
{
    [SerializeField] private Useable m_ItemToTransform;
    [SerializeField] private Vector3 m_TransformOffsetPosition;

    private int _TimeToTrigger;
    private int _TriggerCounter;
    private int _LastUpdateTrigger;
    private bool _Counting;
    private bool _IsPaused;

    private TransformConfig _TargetTransformConfig;

    private void Start()
    {
        Messenger.AddListener<int>(TimeManager.AdvanceTimeString, UpdateCounter);
    }

    public void StartTrigger(TransformConfig config)
    {
        StopTrigger();

        _TargetTransformConfig = config;
        _TriggerCounter = 0;
        _Counting = true;
        _LastUpdateTrigger = TimeManager.TotalHours;
        _TimeToTrigger = _TargetTransformConfig.TimeToTrigger;
    }

    public void StopTrigger()
    {
        _Counting = false;
        _TargetTransformConfig = null;
    }

    public void UpdateCounter(int time)
    {
        if (_IsPaused)
            return;

        if (!_Counting)
            return;

        _TriggerCounter += TimeManager.TotalHours - _LastUpdateTrigger;
        _LastUpdateTrigger = TimeManager.TotalHours;

        if (_TriggerCounter >= _TimeToTrigger)
        {
            //Messenger.Broadcast<string>(m_ItemToTransform.SetName, _TargetTransformConfig.Transformations.UseableName);

            _Counting = false;
        }
    }

    public void Pause(bool pause) => _IsPaused = pause;

    private void OnDrawGizmos()
    {
        if(m_ItemToTransform != null)
            Gizmos.DrawWireSphere(m_ItemToTransform.transform.position + m_TransformOffsetPosition, 0.1f);
    }
}