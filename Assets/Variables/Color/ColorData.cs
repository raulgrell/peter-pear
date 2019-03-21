using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType
{
    Gray,
    Red,
    Green,
    Blue,
    Yellow
}

[CreateAssetMenu(menuName = "Cross/Color")]
public class ColorData : ScriptableObject
{
    public Color color;
    public ColorType colorType;
}
