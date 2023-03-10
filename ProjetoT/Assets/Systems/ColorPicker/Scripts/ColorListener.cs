using UnityEngine;
using UnityEngine.UI;

public class ColorListener : MonoBehaviour
{
    public ColorKey ColorKey;
    private Image _Image;

    private void Awake()
    {
        _Image = GetComponent<Image>();
        SetupColor();
    }

    public void SetupColor()
    {
        _Image.color = ColorPicker.Instance.GetColor(ColorKey);
    }

    public void SetColor(ColorKey key)
    {
        ColorKey = key;
        SetupColor();
    }
}