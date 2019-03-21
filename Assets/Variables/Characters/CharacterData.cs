using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cross/Character")]
public class CharacterData: ScriptableObject
{
    public string displayName;
    public Sprite dialogImage;
    public Sprite gameImage;
}
