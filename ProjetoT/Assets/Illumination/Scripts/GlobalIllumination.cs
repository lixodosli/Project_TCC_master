using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalIllumination : MonoBehaviour
{
    private Light[] _Lights;
    public Gradient ColorDurigTheDay;
    public float TimeToChangeColor;

    private float _Counter;
    private float _PercentageComplete;
    private bool _IsCounting;
    private Color _CorInicial;
    private Color _CorFinal;

    private void Awake()
    {
        _Lights = GetComponentsInChildren<Light>();
        Messenger.AddListener<int>(TimeManager.AdvanceTimeString, UpdateColor);
    }

    private void Start()
    {
        UpdateColor(0);
    }

    private void Update()
    {
        if (_IsCounting)
        {
            _Counter += Time.deltaTime;
            _PercentageComplete = _Counter / TimeToChangeColor;

            if(_PercentageComplete >= 1)
            {
                _IsCounting = false;
                _PercentageComplete = 1;
            }

            foreach (Light light in _Lights)
            {
                light.color = Color.Lerp(_CorInicial, _CorFinal, _PercentageComplete);
            }
        }
    }

    private void UpdateColor(int time)
    {
        float gradientColor = (float)TimeManager.CurrentHour / 24f;

        _CorInicial = _Lights[0].color;
        _CorFinal = ColorDurigTheDay.Evaluate(gradientColor);
        _Counter = 0;
        _IsCounting = true;
    }
}