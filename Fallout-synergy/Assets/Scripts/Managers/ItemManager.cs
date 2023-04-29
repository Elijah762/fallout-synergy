using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemManager : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform _rectTransform;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        _rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
    }
}
