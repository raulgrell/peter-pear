using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Start,
    Choose,
    Check,
    Match,
    Wrong,
    LevelUp
}

public class Session : MonoBehaviour
{
    public Level[] levels;
    public PlayerController player;
    public Sprite simonFace;
    public int currentLevel;

    private GameState state;
    private PlayerUI ui;
    private CardSlot firstSlot;
    private CardSlot secondSlot;
    private Coroutine checker;

    private readonly List<CardData> cards = new List<CardData>();
    private readonly List<CardSlot> cardSlots = new List<CardSlot>();

    void Start()
    {
        state = GameState.Start;
        
        ui = GetComponent<PlayerUI>();
        ui.ShowConversation();
        
        GenerateCards(currentLevel);
    }

    void Update()
    {
        switch (state)
        {
            case GameState.Start:
                break;
            case GameState.Choose:
                break;
            case GameState.Check:
                break;
            case GameState.Match:
                break;
            case GameState.Wrong:
                break;
            case GameState.LevelUp:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        if (Input.GetButtonDown("Select"))
        {
            if (checker != null) StopCoroutine(checker);
            checker = StartCoroutine(SelectCardUnderPlayer());
        }
    }

    public void GenerateCards(int levelIndex)
    {
        cards.Clear();
        cardSlots.Clear();

        for (int i = 0; i < levelIndex + 1; i++)
        {
            var level = levels[i];

            for (int j = 0; j < level.cards.Length; j++)
            {
                cards.Add(level.cards[j]);
                cards.Add(level.cards[j]);
            }

            for (int j = 0; j < level.slots.Length; j++)
            {
                cardSlots.Add(level.slots[j]);
            }
        }

        cards.Shuffle();

        Debug.Assert(cardSlots.Count == cards.Count);

        for (int i = 0; i < cardSlots.Count; i++)
        {
            var cardSlot = cardSlots[i];
            cardSlot.card = cards[i];
            cardSlot.Reset();
        }
    }

    public void ActivateNextLevel()
    {
        currentLevel += 1;
        
        ui.ShowNextConversation();

        GenerateCards(currentLevel);
        
        foreach (CardSlot cardSlot in cardSlots)
        {
            cardSlot.Reset();
        }
    }

    public IEnumerator SelectCardUnderPlayer()
    {
//        switch (state)
//        {
//            case GameState.Start:
//                break;
//            case GameState.Choose:
//                break;
//            case GameState.Check:
//                break;
//            case GameState.Match:
//                break;
//            case GameState.Wrong:
//                break;
//            case GameState.LevelUp:
//                break;
//            default:
//                throw new ArgumentOutOfRangeException();
//        }

        if (player.currentSlot == null)
        {
            Debug.Log("Not on a card");
            yield break;
        }

        if (player.currentSlot.solved)
        {
            Debug.Log("Solved");
            yield break;
        }

        if (firstSlot == null)
        {
            firstSlot = player.currentSlot;
            firstSlot.Reveal();
            Debug.Log("First card selected");
            yield break;
        }

        if (firstSlot == player.currentSlot)
        {
            firstSlot.Place();
            firstSlot = null;
            Debug.Log("Cancel selection");
            yield break;
        }

        if (secondSlot != null)
        {
            firstSlot = null;
            secondSlot = null;
            Debug.Log("Already checking");
            yield break;
        }

        secondSlot = player.currentSlot;
        secondSlot.Reveal();

        Debug.Log("Second Card Selected;");

        if (firstSlot.card.type == CardType.Bomb && secondSlot.card.type == CardType.Bomb)
        {
            StartCoroutine(ui.ShowMessage("Recurso!!!", simonFace));
        }

        if (SelectedCardsMatch())
        {
            Debug.Log("Found the pair!");
            firstSlot.solved = true;
            secondSlot.solved = true;

            cards.Remove(firstSlot.card);
            cards.Remove(secondSlot.card);

            firstSlot = null;
            secondSlot = null;

            if (cards.Count == 0)
            {
                Debug.Log("NOICE!");
                yield return new WaitForSeconds(1.0f);
                ActivateNextLevel();
            }

            yield return new WaitForSeconds(1.0f);
        }
        else
        {
            Debug.Log("WRONG!");
            yield return new WaitForSeconds(1.0f);

            firstSlot.Place();
            secondSlot.Place();

            firstSlot = null;
            secondSlot = null;
        }
    }

    private bool SelectedCardsMatch()
    {
        if (firstSlot == null || secondSlot == null)
        {
            Debug.Log("Not enough cards");
            return false;
        }

        if (firstSlot.card != secondSlot.card)
        {
            Debug.Log("NOPE");
            return false;
        }

        return true;
    }
}