using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FeedbackMessage : MonoBehaviour
{
    public static FeedbackMessage Instance;

    [SerializeField] private TextMeshProUGUI m_Message;
    [SerializeField] private CanvasGroup m_Box;
    [SerializeField] private Gradient m_Alpha;
    [SerializeField] private float m_TimePerMessageLetter;
    private Counter TimeCounter;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(TimeCounter != null)
        {
            m_Box.alpha = m_Alpha.Evaluate(TimeCounter.PercentageComplete).a;
            TimeCounter.Update();
        }
        else
            m_Box.alpha = 0;
    }

    public static void ShowFeedback(string message)
    {
        FeedbackMessage m = FindObjectOfType<FeedbackMessage>();

        m.StartFeedback(message);
    }

    public void StartFeedback(string message)
    {
        if (TimeCounter != null)
            StopFeedback(true);

        TimeCounter = new Counter("TimeToDesappear", message.Length * m_TimePerMessageLetter);
        Messenger.AddListener<bool>(TimeCounter.CounterName, StopFeedback);
        TimeCounter.Play();
    }

    private void StopFeedback(bool stop)
    {
        if (stop == false)
            return;

        Messenger.RemoveListener<bool>(TimeCounter.CounterName, StopFeedback);
        TimeCounter = null;
    }
}