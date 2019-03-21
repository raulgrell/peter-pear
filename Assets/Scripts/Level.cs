using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Level : MonoBehaviour
{
    public CardData[] cards;
    public CardSlot[] slots;
    
    public GameObject ladder;
    public bool hideLadder;
    public int ladderSlotIndex;
    
    public GameObject column;
    public bool hideColumn;
    public int columnSlotIndex;
    
    public int holeSlot;
    
    void Start()
    {
        UpdateObstacles();
    }

    void UpdateObstacles()
    {
        ladder.SetActive(!hideLadder && ladderSlotIndex > 0);
        var ladderPos = slots[ladderSlotIndex].transform.localPosition;
        ladder.transform.localPosition = new Vector3(ladderPos.x - 8, ladderPos.y, ladderPos.z);
        
        column.SetActive(!hideColumn && columnSlotIndex > 0);
        var columnPos = slots[columnSlotIndex].transform.localPosition;
        column.transform.localPosition = new Vector3(columnPos.x, columnPos.y, columnPos.z);
    }

    private void OnValidate()
    {
        UpdateObstacles();
    }
}
