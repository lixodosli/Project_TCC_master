using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Hour;
    [SerializeField] private TextMeshProUGUI Days;
    [SerializeField] private CanvasGroup FeedbackDisplay;
    [SerializeField] private Transform StartPos;
    [SerializeField] private Transform EndPos;
    [SerializeField] private float AnimationTime;
    [SerializeField] private Gradient AnimationAlpha;

    private bool _IsCounting;
    private float _Counter = 0;
    private float _PercentageComplete;


    private void Awake()
    {
        Messenger.AddListener<int>(TimeManager.AdvanceTimeString, UpdateDisplay);
    }

    private void Update()
    {
        UpdateFeedback();
    }

    private void UpdateDisplay(int time)
    {
        Hour.text = TimeManager.CurrentHour.ToString("D2") + ":00";
        Days.text = "Dia " + TimeManager.CurrentDay.ToString("D2");

        StartFeedback(time);
    }

    private void StartFeedback(int time)
    {
        _Counter = 0;
        _IsCounting = true;
        FeedbackDisplay.GetComponent<TextMeshProUGUI>().text = $"+ {time.ToString("D2")}:00";
    }

    private void UpdateFeedback()
    {
        if (!_IsCounting)
        {
            FeedbackDisplay.alpha = 0;
            return;
        }

        _Counter += Time.deltaTime;
        _PercentageComplete = _Counter / AnimationTime;

        if (_PercentageComplete >= 1)
        {
            _Counter = AnimationTime;
            _PercentageComplete = 1;
            _IsCounting = false;
        }

        FeedbackDisplay.transform.position = Vector3.Slerp(StartPos.position, EndPos.position, _PercentageComplete);
        float alphaValue = AnimationAlpha.Evaluate(_PercentageComplete).a;
        FeedbackDisplay.alpha = alphaValue;
    }
}