using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    public static ColorPicker Instance;
    public ColorSet ColorSettings;

    private void Awake()
    {
        Instance = this;
    }

    public Color32 GetColor(ColorKey key)
    {
        return ColorSettings.GetColor(key);
    }
}