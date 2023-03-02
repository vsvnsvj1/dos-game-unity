using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfoScr : MonoBehaviour
{
    public Card selfCard;
    public Image Logo;
    public TextMeshProUGUI Name;

    public void HideCardInfo(Card card)
    {
        selfCard = card;
        Name.text = "";
        Logo.sprite = null;
    }

    public void ShowCardInfo(Card card)
    {
        selfCard = card;
        Name.text = card.Name;
        Logo.sprite = card.Logo;
        Logo.preserveAspect = true; // сохранение стророн спрайта
    }

    public void Start()
    {
       // ShowCardInfo(CardManager.allCards[transform.GetSiblingIndex()]);
    }
}
