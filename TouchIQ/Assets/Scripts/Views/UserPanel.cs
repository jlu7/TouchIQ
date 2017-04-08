using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserPanel : MonoBehaviour
{
    Button ButtonComponent;
    RectTransform ScrollView;
    bool ScrollViewIsVisible = true;

    IEnumerator IEShowScrollView;

    void Start()
    {
        ButtonComponent = transform.Find("BG").GetComponent<Button>();
        ScrollView = this.transform.Find("Mask/ScrollView").GetComponent<RectTransform>();
        ButtonComponent.onClick.AddListener(ShowScrollView);
    }

    // Show is at y = 110
    // Hidden is at y = 850
    public void ShowScrollView()
    {
        if (IEShowScrollView != null)
        {
            StopCoroutine(IEShowScrollView);
        }
        IEShowScrollView = null;
        IEShowScrollView = coShowScrollView();

        StartCoroutine(IEShowScrollView);
    }
    public IEnumerator coShowScrollView()
    {
        int y = 110;

        if (!ScrollViewIsVisible)
        {
            y = 850;
        }

        ScrollViewIsVisible = !ScrollViewIsVisible;

        float speed = 2000;
        Vector2 target = new Vector2(ScrollView.anchoredPosition.x, y);

        while (ScrollView.anchoredPosition.y != y)
        {
            float step = speed * Time.deltaTime;
            ScrollView.anchoredPosition = Vector2.MoveTowards(ScrollView.anchoredPosition, target, step);
            yield return null;
        }

    }
}
