using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popsicle : MonoBehaviour
{
    Button ButtonComponent;
    RectTransform View;
    public bool ViewIsVisible = true;

    IEnumerator IEShowScrollView;

    void Start()
    {
        ButtonComponent = this.transform.Find("Tab").GetComponent<Button>();
        ButtonComponent.onClick.AddListener(ShowScrollView);
        View = this.GetComponent<RectTransform>();
    }

    // Show is at y = 0
    // Hidden is at y = -406
    public void ShowScrollView()
    {
        SoundManager.GetInstance().PlaySingle("SoundFX/digi_plink");

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
        int y = 0;

        if (ViewIsVisible)
        {
            y = -406;
        }

        ViewIsVisible = !ViewIsVisible;
        Debug.Log(ViewIsVisible);

        float speed = 2000;
        Vector2 target = new Vector2(View.anchoredPosition.x, y);

        while (View.anchoredPosition.y != y)
        {
            float step = speed * Time.deltaTime;
            View.anchoredPosition = Vector2.MoveTowards(View.anchoredPosition, target, step);
            yield return null;
        }
    }
}
