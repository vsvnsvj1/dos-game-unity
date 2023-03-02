using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum FieldType
{
    SELF_HAND,
    ENEMY_HAND,
    GAME_FIELD
}

public class DropPlaceScr : MonoBehaviour, IDropHandler
{
    public FieldType type;

    public void OnDrop(PointerEventData eventData)
    {
        if (type != FieldType.GAME_FIELD) return;
        CardMovementScr card = eventData.pointerDrag.GetComponent<CardMovementScr>();

        if (card)
            card.DefaultParent = transform;
    }

}
