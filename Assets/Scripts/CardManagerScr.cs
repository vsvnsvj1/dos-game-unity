using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Card
{

    public string Name;
    public Texture2D Logo;
    public int Number;
    public String Color;

    public Card(string name, string logoPath,int number,string color)
    {
        Name = name;
        Logo = Resources.Load(logoPath) as Texture2D;
        Number = number;
        Color = color;
    }
}

public static class CardManager
{
    public static List<Card> AllCards = new List<Card>();
}

public class CardManagerScr : MonoBehaviour
{
    public void Awake()
    {
        for (int i = 0; i < 11; i++)
        {
            String curNum = i.ToString();
            switch (i)
            {
                case 0:
                    for (int j = 0; j < 2; j++)
                    {
                        CardManager.AllCards.Add(new Card("#", "RedCard", i, "red"));
                        CardManager.AllCards.Add(new Card("#", "GreenCard", i, "green"));
                        CardManager.AllCards.Add(new Card("#", "BlueCard", i, "blue"));
                        CardManager.AllCards.Add(new Card("#", "YellowCard", i, "yellow"));
                    }

                    break;
                case 1:
                case 3:
                case 4:
                case 5:
                    for (int j = 0; j < 3; j++)
                    {
                        CardManager.AllCards.Add(new Card(curNum, "RedCard", i, "red"));
                        CardManager.AllCards.Add(new Card(curNum, "GreenCard", i, "green"));
                        CardManager.AllCards.Add(new Card(curNum, "BlueCard", i, "blue"));
                        CardManager.AllCards.Add(new Card(curNum, "YellowCard", i, "yellow"));
                    }

                    break;
                case 2:
                    for (int j = 0; j < 12; j++)
                    {
                        CardManager.AllCards.Add(new Card(curNum, "BlackCard", i, "black"));
                    }

                    break;
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    for (int j = 0; j < 2; j++)
                    {
                        CardManager.AllCards.Add(new Card(curNum, "RedCard", i, "red"));
                        CardManager.AllCards.Add(new Card(curNum, "GreenCard", i, "green"));
                        CardManager.AllCards.Add(new Card(curNum, "BlueCard", i, "blue"));
                        CardManager.AllCards.Add(new Card(curNum, "YellowCard", i, "yellow"));
                    }

                    break;
            }
        }
    }
}
