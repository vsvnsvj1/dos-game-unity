using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BufferFieldLogic : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(GetComponent<CardMovementScr>().GameNanager.IsPlayerTurn.ToString());
        CardInfoScr card = eventData.pointerDrag.GetComponent<CardInfoScr>();
        if (card)
        {
            GetComponent<CardMovementScr>().GameNanager.CardsDeal();
        }
    }
}
