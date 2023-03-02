using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game
{
    public List<Card> SelfHand, EnemyHand,
                       GameField, GameDeck;
    public Game()
    {
        GameDeck = GiveDeckCard();

        SelfHand = new List<Card>();
        EnemyHand = new List<Card>();

        GameField = new List<Card>();
    }

    List<Card> GiveDeckCard()
    {
        List<Card> list = new List<Card>();

        for (int i = 0; i < CardManager.allCards.Count - 1; i++)
            list.Add(CardManager.allCards[Random.Range(0, CardManager.allCards.Count)]);
        return list;
    }
}


public class GameManagerScr : MonoBehaviour
{
    public Game currentGame;
    public Transform SelfHand, EnemyHand;
    public GameObject CardPref;

    
    void Start()
    {
        currentGame = new Game();
        GiveHandCards(ref currentGame.GameDeck, SelfHand);
        int i = 0;
        while (i++ < 7) currentGame.GameDeck.RemoveAt(0);
        GiveHandCards(ref currentGame.GameDeck, EnemyHand);
        
    }
    void GiveHandCards(ref List<Card> deck, Transform hand)
    {
        int i = 0;
        while (i++ < 7)
            GiveCardToHand(ref deck, hand);
    }
    void GiveCardToHand(ref List<Card> deck, Transform hand)
    {
        if (deck.Count == 0) return;

        Card card = deck[0];
        GameObject CardGo = Instantiate(CardPref, hand, false);
        if (hand == EnemyHand)
            CardGo.GetComponent<CardInfoScr>().HideCardInfo(card);
        else
            CardGo.GetComponent<CardInfoScr>().ShowCardInfo(card);
        deck.RemoveAt(0);
    }
    
}
