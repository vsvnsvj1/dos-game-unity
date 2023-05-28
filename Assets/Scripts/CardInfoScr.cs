using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfoScr : MonoBehaviour
{
    public Card selfCard;
    public new TextMeshProUGUI name;
    public RawImage logo;
    public GameObject HideObj;

    public void ShowCardInfo(Card card)
    {
        HideObj.SetActive(false);
        selfCard = card;
        logo.texture = card.Logo;
        name.text = card.Name;
    }

    public void HideCardInfo(Card card)
    {
        selfCard = card; 
        HideObj.SetActive(true);
    }


    public void Start()
    {
        //ShowCardInfo(CardManager.AllCards[transform.GetSiblingIndex()]);
    }
}
