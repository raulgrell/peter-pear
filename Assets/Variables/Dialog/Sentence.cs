using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cross/Sentence")]
public class Sentence : ScriptableObject
{
    public CharacterData character;
    
    [Multiline]
    public string text;
}
