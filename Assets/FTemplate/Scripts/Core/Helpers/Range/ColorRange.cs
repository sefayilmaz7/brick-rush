using System.Collections;
using UnityEngine;

[System.Serializable]
public class ColorRange
{
    public Color startColor;
    public Color endColor;

    public Color GetValueAtPercent(float value)
    {
        return Color.Lerp(startColor, endColor, value);
    }
}