using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragSlot : MonoBehaviour, IDropHandler
{
    public delegate void MyDelegate();
    public MyDelegate method;

    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(name);
        if (!item)
        {
            if (DragHandler.itemBeingDragged != null)
            {
                method();
            }
        }
    }
}
