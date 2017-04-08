using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Vector3 Difference = new Vector3();

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        startPosition = transform.GetComponent<RectTransform>().anchoredPosition;
        Difference = transform.position - Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + Difference;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        transform.GetComponent<RectTransform>().anchoredPosition = startPosition;
    }
}
