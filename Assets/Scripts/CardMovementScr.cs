using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CardMovementScr : MonoBehaviour,IBeginDragHandler, IDragHandler,IEndDragHandler
{   
    Camera MainCamera;
    Vector3 offset;
    public GameManagerScr GameNanager;
    public Transform DefaultParent;
    public bool IsDraggable;
    
    void Awake()
    {
       
        MainCamera = Camera.allCameras[0];
        GameNanager = FindObjectOfType<GameManagerScr>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);

        DefaultParent = transform.parent;

        IsDraggable = DefaultParent.GetComponent<DropPlaceScr>().type == FieldType.SELF_HAND && GameNanager.IsPlayerTurn;
        if (!IsDraggable)
        {
            Debug.Log("IsDraggable is false");
            return;

        }

        transform.SetParent(DefaultParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
     
        
    }
    

    public void OnDrag(PointerEventData eventData)
    { 
        if (!IsDraggable) return;
        Vector3 newPos = MainCamera.ScreenToWorldPoint(eventData.position);
        transform.position = newPos + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!IsDraggable) return;
        transform.SetParent(DefaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
   
    }

}
