using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Despertador : Interactable
{
    [SerializeField] private int[] m_HoursToWakeUp;
    [SerializeField] private TMP_Text m_HoursDisplay;
    private int _CurrentHourIndex = 0;
    public static int HoursToWakeUp;

    private void Update()
    {
        string formattedHour = m_HoursToWakeUp[_CurrentHourIndex].ToString("00") + ":00"; // Format as 00:00
        m_HoursDisplay.text = formattedHour;

        HoursToWakeUp = m_HoursToWakeUp[_CurrentHourIndex];
    }

    public override void DoInteraction()
    {
        _CurrentHourIndex = (_CurrentHourIndex + 1) % m_HoursToWakeUp.Length;
    }
}