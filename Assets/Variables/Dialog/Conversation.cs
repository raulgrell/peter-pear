using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Cross/Conversation")]
public class Conversation : ScriptableObject
{
    [ReorderableList]
    public List<Sentence> sentences;
}
