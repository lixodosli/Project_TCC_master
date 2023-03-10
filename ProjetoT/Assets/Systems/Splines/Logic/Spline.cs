using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour
{
    public delegate void SplineCallback(Spline spline);
    public SplineCallback OnStartSpline;
    public SplineCallback OnEndSpline;

    [SerializeField] private QBezier[] m_Bezier;
    private float _Timer;
    private float _Speed = 1f;
    private bool _Running;
    private bool _Loopable;

    private void Update()
    {
        if (_Running)
        {
            _Timer += Time.deltaTime * _Speed;

            if (_Timer > m_Bezier.Length)
            {
                if (_Loopable)
                {
                    _Timer = 0;
                }
                else
                {
                    _Timer = m_Bezier.Length - 1;
                    _Running = false;
                }

                OnEndSpline?.Invoke(this);
            }
        }
    }

    public void StartSplineAnim(bool loopAble, float speed)
    {
        _Timer = 0f;
        _Speed = speed;
        _Loopable = loopAble;
        _Running = true;

        OnStartSpline?.Invoke(this);
    }

    public void StartSplineAnim(bool loopAble)
    {
        _Timer = 0f;
        _Loopable = loopAble;
        _Running = true;

        OnStartSpline?.Invoke(this);
    }

    public Vector3 MiddlePoint()
    {
        return m_Bezier[(int)_Timer].MiddlePoint(_Timer % 1);
    }
}