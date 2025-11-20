using System;
using UnityEngine;

[Serializable]
public struct SerializableColor
{
    public float r;
    public float g;
    public float b;
    public float a;

    public SerializableColor(Color color)
    {
        r = color.r;
        g = color.g;
        b = color.b;
        a = color.a;
    }

    public Color ConvertToUnityColor()
    {
        return new Color(r, g, b, a);
    }
}
