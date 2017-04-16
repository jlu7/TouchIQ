using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserPanel : MonoBehaviour
{
    Button ButtonComponent;
    RectTransform ScrollView;
    bool ScrollViewIsVisible = true;
    CanvasRenderer LocalViewCanvas;

    IEnumerator IEShowScrollView;

    void Start()
    {
        ButtonComponent = transform.Find("BG").GetComponent<Button>();
        ScrollView = this.transform.Find("Mask/ScrollView").GetComponent<RectTransform>();
        ButtonComponent.onClick.AddListener(ShowScrollView);
        LocalViewCanvas = transform.Find("BG/User").GetComponent<CanvasRenderer>();

        VideoChat.localView = true;
        if(Application.platform == RuntimePlatform.Android)
        {
            LocalViewCanvas.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 90));
        }

        PhotoController.GetInstance().OnActiveSetChanged += ForceOpenScrollView;
    }

    // Show is at y = 110
    // Hidden is at y = 850
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

    private void ForceOpenScrollView(PhotoSet set)
    {
        if (ScrollViewIsVisible)
        {
            ShowScrollView();
        }
    }

    void Update()
    {
        
        if (null != VideoChat.localViewTexture)
            LocalViewCanvas.SetTexture(VideoChat.localViewTexture);
    }
}
