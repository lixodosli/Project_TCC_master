using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungrySystem : MonoBehaviour
{
    [SerializeField] private Image m_BarFill;
    public static string HungryCountName { get; private set; } = "HungryCount";
    public static string HungryName { get; private set; } = "Hungry";
    public int CurrentHungry { get; private set; } = 6;
    public int MaxHungry { get; private set; } = 6;
    public float HungryCount { get; private set; } = 0f;

    private void Start()
    {
        UpdateBar();
        Messenger.AddListener<float>(HungryCountName, ChangeHungryCount);
        Messenger.AddListener<int>(HungryName, IncreaseHungry);
    }

    public void ChangeHungryCount(float change)
    {
        HungryCount += change;

        if(HungryCount >= 100)
        {
            IncreaseHungry(((int)HungryCount / 100) * -1);
            HungryCount = 0;
        }
    }

    public void IncreaseHungry(int increase)
    {
        CurrentHungry += increase;

        if(CurrentHungry >= MaxHungry)
        {
            CurrentHungry = MaxHungry;
        }
        else if (CurrentHungry < 0)
        {
            // Start Counters To Die
            // Display Feedback
            Debug.Log("You die");
        }

        UpdateBar();
    }

    private void UpdateBar() => m_BarFill.fillAmount = (float)CurrentHungry / MaxHungry;
}