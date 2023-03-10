using UnityEngine;
using UnityEngine.Events;

public class AnimThroughtSpline : MonoBehaviour
{
    [SerializeField] private float m_AnimSpeed;
    [SerializeField] private bool m_Loopable = true;
    [SerializeField] private bool m_UseSpeed = true;
    [SerializeField] private bool m_PlayWhenStart = true;
    [SerializeField] private Spline m_Spline;
    [SerializeField] private UnityEvent m_OnStartAnim;
    [SerializeField] private UnityEvent m_OnEndAnim;

    private void Awake()
    {
        m_Spline.OnStartSpline += StartAnim;
        m_Spline.OnEndSpline += EndAnim;
    }

    private void OnDestroy()
    {
        m_Spline.OnStartSpline -= StartAnim;
        m_Spline.OnEndSpline -= EndAnim;
    }

    private void Start()
    {
        if (m_PlayWhenStart)
        {
            PlayAnim();
        }
    }

    private void Update()
    {
        transform.position = m_Spline.MiddlePoint();
    }

    private void StartAnim(Spline spline)
    {
        if (spline != m_Spline)
            return;

        m_OnStartAnim?.Invoke();
    }

    private void EndAnim(Spline spline)
    {
        if (spline != m_Spline)
            return;

        m_OnEndAnim?.Invoke();
    }

    public void PlayAnim()
    {
        if (m_UseSpeed)
            m_Spline.StartSplineAnim(m_Loopable, m_AnimSpeed);
        else
            m_Spline.StartSplineAnim(m_Loopable);
    }
}