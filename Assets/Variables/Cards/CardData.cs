using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Bomb,
    Ankh,
    Line,
    Triangle
}

[CreateAssetMenu(menuName = "Cross/Card")]
public class CardData : ScriptableObject
{
    public string displayName;
    public Sprite symbol;
    public CardType type;
    public Color color;
}
