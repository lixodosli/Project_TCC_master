using UnityEngine;

public enum ColorKey
{
    White,
    OffWhite,
    Brick,
    Yolk,
    Sand
}

[CreateAssetMenu(menuName = "Scriptable Objects/ColorSet/New Color", fileName = "ColorSet_")]
public class ColorSet : ScriptableObject
{
    public ColorSetting[] Colors;

    public Color32 GetColor(ColorKey key)
    {
        int index = -1;

        for (int i = 0; i < Colors.Length; i++)
        {
            if (Colors[i].Key == key)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
            return Color.magenta;

        return Colors[index].Color;
    }
}

[System.Serializable]
public struct ColorSetting
{
    public ColorKey Key;
    public Color32 Color;
}