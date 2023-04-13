using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DaySystem : MonoBehaviour
{
    public static DaySystem Instance;

    public delegate void DaySystemCallback(int newDay);
    public DaySystemCallback OnDayStart;
    public DaySystemCallback OnDayEnd;

    [SerializeField] private TMP_Text m_FeedcackDisplay;

    private int _DayCount = 0;
    public int DayCount => _DayCount;

    private bool _CallForEndDay = false;

    private void Awake()
    {
        Instance = this;
        SceneTransition.Instance.OnSceneTransitionStart += UpdateDay;
    }

    private void OnDestroy()
    {
        SceneTransition.Instance.OnSceneTransitionStart -= UpdateDay;
    }

    private void Update()
    {
        m_FeedcackDisplay.text = "Dia: " + DayCount.ToString();
    }

    public void RaiseEndDay(int newDay)
    {
        if (_CallForEndDay)
            return;

        _DayCount++;
        SetEndDayTrue();
        SceneTransition.Instance.StartFadeIn();
    }

    private void UpdateDay()
    {
        OnDayEnd?.Invoke(DayCount);
        StartCoroutine(nameof(UpdateDayCoroutine));
    }

    private IEnumerator UpdateDayCoroutine()
    {
        if (OnDayEnd != null)
        {
            Delegate[] invocationList = OnDayEnd.GetInvocationList();
            int eventCount = 0;

            while (eventCount < invocationList.Length)
            {
                Debug.Log("Tentando Invocar todos os eventos. Ainda há " + (invocationList.Length - eventCount) + " eventos na lista");
                yield return null;

                // Verifica quantas funções foram executadas
                eventCount = 0;
                foreach (Delegate d in invocationList)
                {
                    if (d != null)
                        eventCount++;
                }
            }

            Debug.Log("Todas as funcoes executadas");
            StartNewDay(1f);
        }
        else
        {
            Debug.Log("Nao existem funcoes a serem executadas");
            StartNewDay(1f);
        }
    }

    private void StartNewDay(float time)
    {
        Invoke(nameof(StartFadeOut), time);
        Invoke(nameof(SetEndDayFalse), time);
        Invoke(nameof(StartDay), time);
    }

    private void StartFadeOut() => SceneTransition.Instance.StartFadeOut();

    private void StartDay() => OnDayStart?.Invoke(DayCount);

    private void SetEndDayTrue() => _CallForEndDay = true;

    private void SetEndDayFalse() => _CallForEndDay = false;
}