using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public enum CardState
{
    Hidden,
    Seen,
    Selected,
    Solved
}

public class CardSlot : MonoBehaviour
{
    public CardData card;
    public SpriteRenderer symbol;
    public SpriteRenderer color;
    public GameObject back;
    public GameObject front;
    
    public CardState state;

    internal bool seen;
    internal bool solved;
    internal bool hot;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hot = true;
            var player = other.gameObject.GetComponent<PlayerController>();
            player.currentSlot = this;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hot = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (hot)
        {
            Gizmos.DrawWireCube(transform.position, Vector3.one);
        }
    }

    public void Place()
    {
        back.SetActive(false);
        front.SetActive(true);
    }

    public void Reveal()
    {
        back.SetActive(true);
        front.SetActive(false);
        seen = true;
    }

    public void Reset()
    {
        Place();
        seen = false;
        solved = false;
        symbol.sprite = card.symbol;
        color.color = card.color;
    }
}
