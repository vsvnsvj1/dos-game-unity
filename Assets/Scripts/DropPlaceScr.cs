using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum FieldType
{
    SELF_HAND,
    ENEMY_HAND,
    GAME_FIELD,
    BUFFER_FIELD
}

public class DropPlaceScr : MonoBehaviour, IDropHandler
{
    public FieldType type;

    public void OnDrop(PointerEventData eventData) 
    {
        //if (type != FieldType.GAME_FIELD) return;
        CardMovementScr card = eventData.pointerDrag.GetComponent<CardMovementScr>();
        
        
        //if (type != FieldType.BUFFER_FIELD  || !card.IsDraggable || !card.GameNanager.IsPlayerTurn) return;
        // не буффер + нельзя трогать + чужой ход
        //if (type == FieldType.GAME_FIELD && !card.GameNanager.CanDropOnGameField) return;
        if ((type == FieldType.BUFFER_FIELD || (type == FieldType.GAME_FIELD && card.GameNanager.CanDropOnGameField()))
            && card.IsDraggable &&
            card.GameNanager.IsPlayerTurn)
        {
            if (card)
            {
                if (type == FieldType.BUFFER_FIELD)
                {
                    if (card.GameNanager.BufferFieldCards.Count < 2 )
                    {
                        card.GameNanager.PlayerHandCards.Remove(card.GetComponent<CardInfoScr>());
                        card.GameNanager.BufferFieldCards.Add(card.GetComponent<CardInfoScr>());
                        card.DefaultParent = transform;
                        card.GameNanager.CardsDeal();
                    }
                }
                else
                {
                    
                        card.GameNanager.PlayerHandCards.Remove(card.GetComponent<CardInfoScr>());
                        card.GameNanager.GameFieldCards.Add(card.GetComponent<CardInfoScr>());
                        card.DefaultParent = transform;
                        card.GameNanager.DecrementBonuses();
                        card.GameNanager.CheckForResult();
                    

                    
                }
            }
        }

       /* if (card && card.GameNanager.BufferFieldCards.Count < 2 )
        {
            card.GameNanager.PlayerHandCards.Remove(card.GetComponent<CardInfoScr>());
            card.GameNanager.BufferFieldCards.Add(card.GetComponent<CardInfoScr>());
            card.DefaultParent = transform;
            card.GameNanager.CardsDeal();
        }*/
    }

}
