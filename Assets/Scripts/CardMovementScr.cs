using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CardMovementScr : MonoBehaviour,IBeginDragHandler, IDragHandler,IEndDragHandler
{   
    Camera MainCamera;
    Vector3 offset;
    public Transform DefaultParent;
    public bool IsDraggable;
    void Awake()
    {
        MainCamera = Camera.allCameras[0];
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);

        DefaultParent = transform.parent;

        IsDraggable = DefaultParent.GetComponent<DropPlaceScr>().type == FieldType.SELF_HAND;
        if (!IsDraggable) return;


        transform.SetParent(DefaultParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

     void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (!IsDraggable) return;
        Vector3 newPos = MainCamera.ScreenToWorldPoint(eventData.position);
        transform.position = newPos + offset;
    }

     void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (!IsDraggable) return;
        transform.SetParent(DefaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

}
