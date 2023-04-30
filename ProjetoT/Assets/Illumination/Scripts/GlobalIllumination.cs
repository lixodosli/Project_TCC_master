using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalIllumination : MonoBehaviour
{
    private Light[] _Lights;
    public Gradient ColorDurigTheDay;

    private void Awake()
    {
        _Lights = GetComponentsInChildren<Light>();
        Messenger.AddListener<int>(TimeManager.AdvanceTimeString, UpdateColor);
    }

    private void Start()
    {
        UpdateColor(0);
    }

    private void UpdateColor(int time)
    {
        float cor = (float)TimeManager.CurrentHour / 24f;

        foreach (Light light in _Lights)
        {
            light.color = ColorDurigTheDay.Evaluate(cor);
        }
    }
}