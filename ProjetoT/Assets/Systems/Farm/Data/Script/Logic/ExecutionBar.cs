using UnityEngine;
using UnityEngine.UI;

public class ExecutionBar : MonoBehaviour
{
    [SerializeField] private Transform m_Bar;
    [SerializeField] private Image m_BarImage;
    [SerializeField] private Transform m_Target;
    [SerializeField] private Vector3 m_Offset;
    [SerializeField] private float m_Speed;

    private bool _Counting;
    private float _Counter;
    private float _PercentageComplete;
    private float _TimeToEnd;

    private float _Cooldown;

    public static event System.Action Complete;
    public static event System.Action Canceled;

    private void Awake()
    {
        PlayerMovement.DoneSomeAction += Cancel;
    }

    private void OnDestroy()
    {
        PlayerMovement.DoneSomeAction -= Cancel;
    }

    private void Update()
    {
        UpdateBar();

        _Cooldown = Mathf.Clamp01(_Cooldown - Time.deltaTime);
    }

    private void UpdateBar()
    {
        if (!_Counting)
            return;

        _Counter += Time.deltaTime;
        _PercentageComplete = _Counter / _TimeToEnd;

        m_BarImage.fillAmount = _PercentageComplete;

        if (_PercentageComplete >= 1)
        {
            _Counter = _TimeToEnd;
            _PercentageComplete = 1;
            _Counting = false;
            m_Bar.gameObject.SetActive(false);
            Complete?.Invoke();
        }
    }

    public static void StartCounting(float time)
    {
        var instance = FindObjectOfType<ExecutionBar>();

        if (instance == null)
        {
            Debug.LogError("ExecutionBar not found in the scene.");
            return;
        }

        instance._Counter = 0;
        instance._TimeToEnd = time;
        instance._Counting = true;
        instance._Cooldown = 0.3f;
        instance.m_Bar.gameObject.SetActive(true);
    }

    private void Cancel()
    {
        if (_Cooldown > 0)
            return;

        Canceled?.Invoke();
        _Counting = false;
        m_Bar.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        if (NearbyUseableSets.ClosestUseableSet != null)
        {
            Vector3 targetPos = Camera.main.WorldToScreenPoint(NearbyUseableSets.ClosestUseableSet.transform.position + m_Offset);
            m_Bar.position = targetPos;
        }
    }
}
