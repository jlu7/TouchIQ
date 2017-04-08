using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragSlot : MonoBehaviour, IDropHandler
{
    public delegate void MyDelegate();
    public MyDelegate method;

    public void OnDrop(PointerEventData eventData)
    {
        method();
    }
}
