using UnityEngine;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Hour;
    [SerializeField] private TextMeshProUGUI Days;

    private void Awake()
    {
        Messenger.AddListener<int>(TimeManager.AdvanceTimeString, UpdateDisplay);
    }

    private void UpdateDisplay(int time)
    {
        Hour.text = TimeManager.CurrentHour.ToString("D2") + ":00";
        Days.text = "Dia " + TimeManager.CurrentDay.ToString("D2");
    }
}