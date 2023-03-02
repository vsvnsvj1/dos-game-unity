using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public struct Card
{
    public string Name;
    //public Image Logo;
    public Sprite Logo;

    public Card(string name, string logoPath)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoPath);
 
            
        
        
    }
}
/*
public static class Extensions
{
    public static void Shuffle<Card>(this List<Card> values)
    {
        for (int i = 0; i < values.Count; i++)
        {
            Card temp = values.ElementAt(i);
            int posSwitch = UnityEngine.Random.Range(0, values.Count);
            values[i] = values[posSwitch];
            values[posSwitch] = temp;
        }
    }
}
*/



public static class CardManager
{
    public static List<Card> allCards = new List<Card>(108);
}

public class CardManagerScr : MonoBehaviour
{
    
    public void Awake()
    {
        for (int i = 0; i < 11; i++)
        {
            string currentNum = i.ToString();
            /*
             В колоде есть обычные цветные карты — их 96 штук. Цвета четыре: красный, синий, зелёный, жёлтый. Каждый цвет — это 24 карты: по три карты с числами 1, 3, 4, 5 и по две карты с числами 6, 8, 9 и 10.
Есть также 12 диких карт. Из них 4 двойки на цветном поле (это карта «дос», которой можно присваивать любой цвет) и 8 карт с решёткой (ей можно присваивать любое число).
            //CardManager.allCards.Add(new Card("xxxx", "black.png"));
            */
            if (i == 0)
            {
                for (int j = 0; j < 2; j++)
                {
                    CardManager.allCards.Add(new Card("Red #", "red.png"));
                    CardManager.allCards.Add(new Card("Green #", "green.png"));
                    CardManager.allCards.Add(new Card("Blue # ", "blue.png"));
                    CardManager.allCards.Add(new Card("Yellow #", "yellow.png"));
                }
            }
            if (i == 2)
            {
                for (int j = 0; j < 4; j++)
                {
                    CardManager.allCards.Add(new Card("Black 2", "black.png"));
                }
            }
            if (i == 1 || i == 3 || i == 4 || i == 5)
            {
                for (int j = 0; j < 3; j++)
                {
                    CardManager.allCards.Add(new Card("Red " + currentNum, "red.png"));
                    CardManager.allCards.Add(new Card("Green " + currentNum, "green.png"));
                    CardManager.allCards.Add(new Card("Blue " + currentNum, "blue.png"));
                    CardManager.allCards.Add(new Card("Yellow " + currentNum, "yellow.png"));
                }
            }
            if (i == 6 || i == 7 || i == 8 || i == 9 || i == 10)
            {
                for (int j = 0; j < 2; j++)
                {
                    CardManager.allCards.Add(new Card("Red " + currentNum, "red.png"));
                    CardManager.allCards.Add(new Card("Green " + currentNum, "green.png"));
                    CardManager.allCards.Add(new Card("Blue " + currentNum, "blue.png"));
                    CardManager.allCards.Add(new Card("Yellow " + currentNum, "yellow.png"));
                }
            }

        }
        //CardManager.allCards.Shuffle();
    }
}