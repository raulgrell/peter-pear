using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SymbolType
{
    Gray,
    Red,
    Green,
    Blue,
    Yellow
}

[CreateAssetMenu(menuName = "Cross/Symbol")]
public class SymbolData : ScriptableObject
{
    public SymbolType type;
}
