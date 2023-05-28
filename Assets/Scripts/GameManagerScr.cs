using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
using Random = UnityEngine.Random;


public class Game
{
    public List<Card> GameDeck;
    public Game()
    {
        GameDeck = GiveDeckCard();
    }

    List<Card> GiveDeckCard()
    {
        //System.Random rng = new System.Random();
        /*
        //List<Card> list = new List<Card>();
        List<Card> list = CardManager.AllCards.OrderBy(x => rng.Next()).ToList();
        for (int i = 0; i < list.Count; i++)
        {
            list = list.OrderBy(x => rng.Next()).ToList();
        }
        /*
        for (int i = 0; i < CardManager.AllCards.Count; i++)
          list.Add(CardManager.AllCards[Random.Range(0, CardManager.AllCards.Count)]);
        */
        //list = CardManager.AllCards;
        
        //List<Card> = 
        System.Random rand = new System.Random();
        List<Card> deck = CardManager.AllCards;
        for (int i = 0; i < deck.Count; i++)
        {
            int j = rand.Next(i, deck.Count);
            (deck[i], deck[j]) = (deck[j], deck[i]);
        }

        return deck;
    }
}


public class GameManagerScr : MonoBehaviour
{
    public Game currentGame;
    public Transform SelfHand, EnemyHand, GameField,BufferField;
    public GameObject CardPref;
    public int Turn, Turntime = 30;
    public Button GiveCardFromDeck, OnEndTurn;
    public TextMeshProUGUI ChangeTimeTXT;
    private int bonus1 = 0, bonus2 = 0;
    private int penalties;

    public GameObject ResultGO;
    public TextMeshProUGUI ResultTXT;


 

    public List<CardInfoScr> PlayerHandCards = new List<CardInfoScr>(),
        EnemyHandCards = new List<CardInfoScr>(),
        GameFieldCards = new List<CardInfoScr>(),
        BufferFieldCards = new List<CardInfoScr>();
    public bool IsPlayerTurn
    {
        get
        {
            return Turn % 2 == 0;
        }
    }



    public void DecrementBonuses()
    {
        if (bonus1 > 0)
        {
            bonus1--;
            return;
        }

        if (bonus2 > 0)
        {
            bonus2--;
        }
        CheckForResult();
    }

    public void restartGame()
    {
        StopAllCoroutines();
        foreach (var card in PlayerHandCards)
        {
            Destroy(card.GameObject());
        }
        foreach (var card in EnemyHandCards)
        {
            Destroy(card.GameObject());
        }
        foreach (var card in BufferFieldCards)
        {
            Destroy(card.GameObject());
        }
        foreach (var card in GameFieldCards)
        {
            Destroy(card.GameObject());
        }
        
        PlayerHandCards.Clear();
        EnemyHandCards.Clear();
        GameFieldCards.Clear();
        BufferFieldCards.Clear();
        
        startGame();

    }

    void startGame()
    {
        ResultGO.SetActive(false);
        currentGame = new Game();
        OnEndTurn.interactable = true;
        GiveCards(ref currentGame.GameDeck, SelfHand,7);
        GiveCards(ref currentGame.GameDeck, EnemyHand, 7);
        GiveCards(ref currentGame.GameDeck, GameField, 2);
        StartCoroutine(TurnFunc());

    }

    void Start()
    {
        startGame();
        /*
        GiveHandCards(ref currentGame.GameDeck, SelfHand);
        int i = 0;
        while (i++ < 7) currentGame.GameDeck.RemoveAt(0);
        GiveHandCards(ref currentGame.GameDeck, EnemyHand);
        */
    }
    
    void GiveCards(ref List<Card> deck, Transform hand,int countOfCards)
    {
        
        for (int i = 0; i < countOfCards; i++)
        {
            GiveCard(ref deck, hand);
        }

    }
    public void GiveCard(ref List<Card> deck, Transform hand)
    {
        Card card = deck[0];
        GameObject cardGo = Instantiate(CardPref, hand, false);
        if (hand == EnemyHand)
            //cardGo.GetComponent<CardInfoScr>().ShowCardInfo(card);
            cardGo.GetComponent<CardInfoScr>().ShowCardInfo(card);
        else
            cardGo.GetComponent<CardInfoScr>().ShowCardInfo(card);
        if (hand == SelfHand)
            PlayerHandCards.Add(cardGo.GetComponent<CardInfoScr>());
        if (hand == EnemyHand) 
            EnemyHandCards.Add(cardGo.GetComponent<CardInfoScr>());
        if (hand == GameField)
            GameFieldCards.Add(cardGo.GetComponent<CardInfoScr>());
        deck.RemoveAt(0);
        deck.Insert(deck.Count, card);
    }

    IEnumerator TurnFunc()
    {
        Turntime = 30;
        ChangeTimeTXT.text = Turntime.ToString();
        if (IsPlayerTurn)
        {
            while (Turntime-- > 0)
            {
                ChangeTimeTXT.text = Turntime.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            while (Turntime-- > 27)
            {
                ChangeTimeTXT.text = Turntime.ToString();
                yield return new WaitForSeconds(1);
            }
            EnemyTurn(EnemyHandCards);
        }
        ChangeTurn();
    }

    void EnemyTurn(List<CardInfoScr> cards)
    {
        
/*        
        List<CardInfoScr> itemsToRemove = new List<CardInfoScr>();
        List<bool> isMatchedalready = new List<bool>();
        bool WereAnyMatches = false;

        foreach (CardInfoScr enemyCard in cards)
        {
            foreach ( CardInfoScr gameFieldCard in GameFieldCards)
            {
                if (enemyCard.selfCard.Number == gameFieldCard.selfCard.Number)
                {
                    itemsToRemove.Add(enemyCard);
                    break;
                }
            }

        for (int i = 0; i < GameFieldCards.Count; i++)
            isMatchedalready.Add(false);
        
            for (int i = 0; i < cards.Count; i++)
        {
            for (int j = 0; j < GameFieldCards.Count; j++)
            {
                if (!isMatchedalready[j] && (cards[i].selfCard.Number == GameFieldCards[j].selfCard.Number || cards[i].selfCard.Number == 0  
                        || GameFieldCards[j].selfCard.Number  == 0 ) )
                {
                    itemsToRemove.Add(cards[i]);
                    isMatchedalready[j] = true;
                    break;
                }
            }
        }

        foreach (CardInfoScr cardToDeal in itemsToRemove)
        {
            WereAnyMatches = true;
            cardToDeal.ShowCardInfo(cardToDeal.selfCard);
            cardToDeal.transform.SetParent(BufferField);
            BufferFieldCards.Add(cardToDeal);
            EnemyHandCards.Remove(cardToDeal);
            CardsDeal();
            Debug.Log("Bot has match with card " + cardToDeal.selfCard.Name);
        }

        while (CanDropOnGameField())
        {
            DecrementBonuses();
            int cardindex = Random.Range(0, EnemyHandCards.Count - 1);
            CardInfoScr card = cards[cardindex];
            card.ShowCardInfo(card.selfCard);
            card.transform.SetParent(GameField);
            GameFieldCards.Add(card);
            EnemyHandCards.Remove(card);
        }
        
        if (!WereAnyMatches && EnemyHandCards.Count < 11)
        {
            Debug.Log("bot hasnt mached anу card");
            int tmp = Random.Range(1, 3);
            Debug.Log("tmp = " + tmp.ToString());
            if (tmp > 1)
                GiveCards(ref currentGame.GameDeck, EnemyHand, 1);
        }


*/ 
        List<List<CardInfoScr>> variants = new List<List<CardInfoScr>>(); 
        List<bool> isMatchedalready = new List<bool>();
        List<bool> checkEnField = new List<bool>();
        for (int i = 0; i < cards.Count; i++)
            checkEnField.Add(false);
        bool WereAnyMatches = false;
        for (int i = 0;i < GameFieldCards.Count;i++)
            isMatchedalready.Add(false);
        
        for (int i = 0; i < cards.Count; i++)
        {
   
            for (int j = i + 1; j < cards.Count; j++)
            {
                for (int gmfcard = 0; gmfcard < GameFieldCards.Count; gmfcard++)
                {
                    if (!isMatchedalready[gmfcard] && !checkEnField[i] && !checkEnField[j] &&
                        ((GameFieldCards[gmfcard].selfCard.Number == (cards[i].selfCard.Number + cards[j].selfCard.Number) && cards[i].selfCard.Number != 0 &&
                          cards[j].selfCard.Number != 0) ||
                         (GameFieldCards[gmfcard].selfCard.Number == 0 && (cards[i].selfCard.Number + cards[j].selfCard.Number) < 10 ) ||
                         (cards[i].selfCard.Number == 0 &&
                          cards[j].selfCard.Number < GameFieldCards[gmfcard].selfCard.Number) ||
                         (cards[j].selfCard.Number == 0 &&
                          cards[i].selfCard.Number < GameFieldCards[gmfcard].selfCard.Number)))
                    {
                        variants.Add(new List<CardInfoScr> { cards[i], cards[j] });
                        if (cards[i].selfCard.Number == 0 || cards[j].selfCard.Number == 0)
                        {
                            Debug.Log(((GameFieldCards[gmfcard].selfCard.Number ==
                                        (cards[i].selfCard.Number + cards[j].selfCard.Number) &&
                                        cards[i].selfCard.Number != 0 &&
                                        cards[j].selfCard.Number != 0)));
                            Debug.Log(GameFieldCards[gmfcard].selfCard.Number == 0);
                            Debug.Log((cards[i].selfCard.Number == 0 &&
                                       cards[j].selfCard.Number < GameFieldCards[gmfcard].selfCard.Number));
                            Debug.Log((cards[j].selfCard.Number == 0 &&
                                       cards[i].selfCard.Number < GameFieldCards[gmfcard].selfCard.Number));
                        }

                        isMatchedalready[gmfcard] = true;
                        checkEnField[i] = true;
                        checkEnField[j] = true;
                        break;
                    }
                }

            }

            for (int gmfcard = 0; gmfcard < GameFieldCards.Count; gmfcard++)
            {
                if ((GameFieldCards[gmfcard].selfCard.Number == cards[i].selfCard.Number ||
                     GameFieldCards[gmfcard].selfCard.Number == 0 || cards[i].selfCard.Number == 0) &&
                    !isMatchedalready[gmfcard] && !checkEnField[i])
                {
                    variants.Add(new List<CardInfoScr> { cards[i] });
                    isMatchedalready[gmfcard] = true;
                    checkEnField[i] = true;
                    break;
                }
            }
        }
        /*
        
        for (int gmfcard = 0; gmfcard < GameFieldCards.Count; gmfcard++)
        {
            isMatchedalready.Add(false);
            for (int i = 0; i < cards.Count; i++)
            {
                bool needtoskip = false;
                for (int j = i + 1; j < cards.Count; j++)
                {
                    
                    if (!isMatchedalready[gmfcard] &&
                        ((GameFieldCards[gmfcard].selfCard.Number ==
                         (cards[i].selfCard.Number + cards[j].selfCard.Number) && cards[i].selfCard.Number != 0 && cards[j].selfCard.Number != 0) ||
                         GameFieldCards[gmfcard].selfCard.Number == 0 ||
                         (cards[i].selfCard.Number == 0 && cards[j].selfCard.Number < GameFieldCards[gmfcard].selfCard.Number  ) ||
                         (cards[j].selfCard.Number == 0 && cards[i].selfCard.Number < GameFieldCards[gmfcard].selfCard.Number )))
                    {
                        variants.Add(new List<CardInfoScr> { cards[i], cards[j] });
                        isMatchedalready[gmfcard] = true;
                        needtoskip = true;
                        break;
                    }

                }

                if ((GameFieldCards[gmfcard].selfCard.Number == cards[i].selfCard.Number ||
                     GameFieldCards[gmfcard].selfCard.Number == 0 || cards[i].selfCard.Number == 0) &&
                    !isMatchedalready[gmfcard] && !needtoskip)
                {
                    variants.Add(new List<CardInfoScr> { cards[i] });
                    isMatchedalready[gmfcard] = true;
                }
            }
        }
        */
        
        foreach (var cardToDealList in variants)
        {
            WereAnyMatches = true;/*
            cardToDeal.ShowCardInfo(cardToDeal.selfCard);
            cardToDeal.transform.SetParent(BufferField);
            BufferFieldCards.Add(cardToDeal);
            EnemyHandCards.Remove(cardToDeal);
            CardsDeal();
            Debug.Log("Bot has match with card " + cardToDeal.selfCard.Name);*/
            String tmp = "";
            foreach (var cardToDeal in cardToDealList)
            {
                cardToDeal.ShowCardInfo(cardToDeal.selfCard);
                cardToDeal.transform.SetParent(BufferField);
                BufferFieldCards.Add(cardToDeal);
                EnemyHandCards.Remove(cardToDeal);
                tmp +=" " + cardToDeal.selfCard.Name + " " + cardToDeal.selfCard.Color;
            }

            tmp += " cards on game field: ";
            foreach (var cc in GameFieldCards)
            {
                tmp += " " + cc.selfCard.Name + " " + cc.selfCard.Color;
            }
            Debug.Log("Bot has match with " + tmp);
           
            
            
            CardsDeal();
        }
        while (CanDropOnGameField() && EnemyHandCards.Count > 0)
        {
            
            DecrementBonuses();
            int cardindex = Random.Range(0, EnemyHandCards.Count - 1);
            CardInfoScr card = cards[cardindex];
            Debug.Log("bot drop for bonus " + card.selfCard.Name + card.selfCard.Color);
            card.ShowCardInfo(card.selfCard);
            card.transform.SetParent(GameField);
            GameFieldCards.Add(card);
            EnemyHandCards.Remove(card);
        }
        
        if (!WereAnyMatches && EnemyHandCards.Count < 11)
        {
            int tmp = Random.Range(1, 3);
            Debug.Log("tmp = " + tmp.ToString());
            if (tmp > 1)
                GiveCards(ref currentGame.GameDeck, EnemyHand, 1);
        }
        
    }
    

    public void OnClickGiveOneCardButton()
    {
       
        StopAllCoroutines();
        GiveCardFromDeck.interactable = IsPlayerTurn;
        if (IsPlayerTurn)
            GiveCard(ref currentGame.GameDeck,SelfHand);
        ChangeTurn();
        StartCoroutine(TurnFunc());
    }

    /*public void Awards()
    {
        Debug.Log("b1 = " + bonus1.ToString() + " b2 = " + bonus2.ToString());
        if (bonus2 + bonus1 != 0)
        {
            
            int tmp = GameFieldCards.Count;
            while (PlayerHandCards.Count > 0 &&(GameFieldCards.Count < tmp + bonus2 + bonus1))
           
            {
                ChangeTimeTXT.text = Turntime.ToString();
                CanDropOnGameField = true;

            }
            GiveCards(ref currentGame.GameDeck,EnemyHand,bonus2);
            CanDropOnGameField = false;
        }

       
    }*/
   
    public bool CanDropOnGameField()
    {
        return (bonus1 + bonus2 > 0);
    }

    
    public void ChangeTurn()
    {
        ClearBufferField();
        RefreshData();
        if (penalties > 0)
        {
            if (IsPlayerTurn)
                GiveCards(ref currentGame.GameDeck,EnemyHand,penalties);
            else 
                GiveCards(ref currentGame.GameDeck,SelfHand,penalties);
        }
           // GiveCards(ref currentGame.GameDeck,EnemyHand,penalties);
        StopAllCoroutines();
        bonus1 = 0;
        bonus2 = 0;
        penalties = 0;
        Turn++;
        GiveCardFromDeck.interactable = IsPlayerTurn;
        OnEndTurn.interactable = IsPlayerTurn;
        StartCoroutine(TurnFunc());
        CheckForResult();
    }

    public void CardsDeal() // проверки на добавление карты 
    {
        List<int> SumsInBuffer = new List<int>(BufferFieldCards.Count);
        int tmp = 0;
        for (int i = 0; i < BufferFieldCards.Count; i++)
        {
            tmp += BufferFieldCards[i].selfCard.Number;
           SumsInBuffer.Add(tmp);
        }
        
        for (int i = 0; i < GameFieldCards.Count; i++)
        {
            if (GameFieldCards[i].selfCard.Number == 0)
            {
                Match(GameFieldCards[i],1);
                return;
            }

            for (int j = 0; j < SumsInBuffer.Count; j++)
            {
                if (BufferFieldCards[j].selfCard.Number == 0)
                {
                    if (SumsInBuffer.Count == 1)
                    {
                        Match(GameFieldCards[i],j + 1);
                        return;
                    }
                    else
                    {
                        if (GameFieldCards[i].selfCard.Number - BufferFieldCards[(j + 1) % 2].selfCard.Number > 0)
                        {
                            Match(GameFieldCards[i],j + 1);
                            return;
                        }
                    }
                }

                if (GameFieldCards[i].selfCard.Number == SumsInBuffer[j])
                {
                    Match(GameFieldCards[i], j + 1);
                    return;
                }
            }

        }

        if (BufferFieldCards.Count == 2)
            ClearBufferField();
    }

    public void Match(CardInfoScr card,int CountOfCardsToMatch)
    {
        
        GiveCardFromDeck.interactable = false;

        if (CountOfCardsToMatch == 1)
        {
            if (card.selfCard.Color == BufferFieldCards[0].selfCard.Color ||
                BufferFieldCards[0].selfCard.Color == "black" || card.selfCard.Color == "black")
            {
                Debug.Log(BufferFieldCards[0].selfCard.Color  + " " + BufferFieldCards[0].selfCard.Name + " has bonused 1");
               
                bonus1++;
            }
             
            
        }
        else
        {
            if ((card.selfCard.Color == BufferFieldCards[0].selfCard.Color ||
                 BufferFieldCards[0].selfCard.Color == "black") &&
                (card.selfCard.Color == BufferFieldCards[1].selfCard.Color ||
                BufferFieldCards[1].selfCard.Color == "black"))
            {
                Debug.Log(BufferFieldCards[0].selfCard.Color  + " " + BufferFieldCards[0].selfCard.Name +" and "+ 
                          BufferFieldCards[1].selfCard.Color + " " + BufferFieldCards[1].selfCard.Name +  " has bonused 2");
                bonus2++;
                penalties++;
            }
        }

        ClearDesk(card);
   
    }

    public void ClearBufferField() // функция очистки буффера
    {
        if (IsPlayerTurn)
        {
           
            for (int i = 0; i < BufferFieldCards.Count(); i++)
            {
                CardInfoScr tmp = BufferFieldCards[0];
                //BufferFieldCards[0].ShowCardInfo(cardToDeal.selfCard);
                if (IsPlayerTurn)
                {
                    tmp.transform.SetParent(SelfHand);
                    PlayerHandCards.Add(tmp);
                }
                else
                {
                    BufferFieldCards[0].HideCardInfo(tmp.selfCard);
                    tmp.transform.SetParent(EnemyHand);
                    EnemyHandCards.Add(tmp);
                }
                BufferFieldCards.Remove(tmp);
            }
            
        }
    }

    public void ClearDesk(CardInfoScr card)

    {
 
        card.GetComponent<CardMovementScr>().OnEndDrag(null);
        for (int i = 0; i < BufferFieldCards.Count; i++)
        {
            //BufferFieldCard[i].GetComponent<CardMovementScr>().OnEndDrag(null);
            Destroy(BufferFieldCards[i].GameObject());
        }
        BufferFieldCards.Clear();
        GameFieldCards.Remove(card);
        Destroy(card.GameObject());
        CheckForResult();
        if (GameFieldCards.Count() == 0 && (bonus1 + bonus2 == 0) && IsPlayerTurn)
            ChangeTurn();
    }

    public void RefreshData()
    {
        if (GameFieldCards.Count < 2)
            GiveCards(ref currentGame.GameDeck,GameField,2 - GameFieldCards.Count);
    }

    public void CheckForResult()
    {
        if (PlayerHandCards.Count == 0 || EnemyHandCards.Count == 0) 
        {
            ResultGO.SetActive(true);
            StopAllCoroutines();
            if (PlayerHandCards.Count == 0)
            {
                ResultTXT.text = "You win";
            }
            else
            {
                ResultTXT.text = "Enemy win";
            }
            restartGame();
        }
        
    }
}
