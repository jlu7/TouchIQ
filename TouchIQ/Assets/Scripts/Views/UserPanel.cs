﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserPanel : MonoBehaviour
{
    Button ButtonComponent;
    RectTransform ScrollView;
    bool ScrollViewIsVisible = false;

    IEnumerator IEShowScrollView;

    void Start()
    {
        ButtonComponent = this.GetComponent<Button>();
        ScrollView = this.transform.Find("Mask/ScrollView").GetComponent<RectTransform>();
        ButtonComponent.onClick.AddListener(ShowScrollView);
    }

    // Show is at y = 110
    // Hidden is at y = 850
    protected void ShowScrollView()
    {
        if (IEShowScrollView != null)
        {
            StopCoroutine(IEShowScrollView);
        }
        IEShowScrollView = null;
        IEShowScrollView = coShowScrollView();

        StartCoroutine(IEShowScrollView);
    }
    protected IEnumerator coShowScrollView()
    {
        int y = 110;

        if (!ScrollViewIsVisible)
        {
            y = 850;
        }

        ScrollViewIsVisible = !ScrollViewIsVisible;
        Debug.Log(ScrollViewIsVisible);

        float speed = 2000;
        Vector2 target = new Vector2(ScrollView.anchoredPosition.x, y);

        while (ScrollView.anchoredPosition.y != y)
        {
            Debug.Log("LDSJF:LKDSJF");
            float step = speed * Time.deltaTime;
            ScrollView.anchoredPosition = Vector2.MoveTowards(ScrollView.anchoredPosition, target, step);
            yield return null;
        }

    }
}
