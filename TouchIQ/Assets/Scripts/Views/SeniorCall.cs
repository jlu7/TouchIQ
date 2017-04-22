using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeniorCall : MonoBehaviour {

    GameObject remoteUserPanel;
    CanvasRenderer calleeCanvas;
    GameObject localUserPanel;
    UserPanel UserPanelRef;
    RectTransform CalleeCanvasZoom;
    Vector2 CalleeOriginalSize;
    GameObject videoCall;
    Text CallTime;

    Button endCall;

    // Use this for initialization
    void Start ()
    {
        remoteUserPanel = transform.Find("Panel").gameObject;
        CalleeCanvasZoom = remoteUserPanel.GetComponent<RectTransform>();
        calleeCanvas = remoteUserPanel.transform.Find("VideoCallee").GetComponent<CanvasRenderer>();
        CalleeOriginalSize = remoteUserPanel.transform.Find("VideoCallee").GetComponent<RectTransform>().sizeDelta;
        CallTime = transform.Find("BottomBar/Tab/Calling").GetComponent<Text>();

        StartCoroutine(CallTimer());

        localUserPanel = transform.Find("UserPanel").gameObject;
        UserPanelRef = localUserPanel.AddComponent<UserPanel>();
        endCall = transform.Find("BottomBar/InCall/EndCall").GetComponent<Button>();
        endCall.onClick.AddListener(() =>
        {
            SoundManager.GetInstance().PlaySingle("SoundFX/pop_drip");
            videoCall.GetComponent<VideoCall>().Restart();
        });

        GameObject videoCallPrefab = Resources.Load<GameObject>("Prefabs/SeniorCall/VideoCall");
        videoCall = GameObject.Instantiate<GameObject>(videoCallPrefab);
        videoCall.transform.SetParent(this.transform);
        videoCall.transform.localPosition = Vector3.zero;

        StartCoroutine(videoCall.GetComponent<VideoCall>().SetupVideo(OnVideoStart));
        DragSlot dragComponent = transform.Find("SharedPhoto/DragSlot").gameObject.AddComponent<DragSlot>();
        dragComponent.method += PhotoDragHandler;
        NetworkController.GetInstance().OnPhotoReceived += ReceivedNetworkPhoto;
    }

    void OnDestroy()
    {
        NetworkController.GetInstance().OnPhotoReceived -= ReceivedNetworkPhoto;
    }

    void OnVideoStart()
    {
        //remoteUserPanel.SetActive(false);
        if (Application.platform != RuntimePlatform.Android)
        {
            calleeCanvas.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 90));
            calleeCanvas.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
        }
    }

    private void ReceivedNetworkPhoto(string setName, string photoName)
    {
        Sprite spr = PhotoController.GetInstance().GetPhoto(setName, photoName);
        SharedPhotoViewOn(spr);
    }

    private void PhotoDragHandler(Sprite spr)
    {
        if (null != PhotoController.GetInstance().ActiveSet && null != spr)
        {
            NetworkController.GetInstance().SendPhotoMessage(PhotoController.GetInstance().ActiveSet.Name, spr.name);
        }
        SharedPhotoViewOn(spr);
    }

    private void SharedPhotoViewOn(Sprite spr)
    {
        ShrinkVideo();
        transform.Find("SharedPhoto/X").gameObject.SetActive(true);
        transform.Find("SharedPhoto/Albums").gameObject.SetActive(true);
        transform.Find("SharedPhoto/DragSlot").gameObject.SetActive(false);
        transform.Find("CloseButton").gameObject.SetActive(true);
        var contentView = transform.Find("SharedPhoto/Albums/Viewport/Content");
        contentView.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -50);
        contentView.Find("Image").GetComponent<Image>().sprite = spr;

        int middle = Mathf.FloorToInt(ViewController.GetInstance().CurrentView.GetComponent<RectTransform>().rect.width);
        Debug.Log(middle);
        transform.Find("SharedPhoto/Albums/Viewport/Content").GetComponent<HorizontalLayoutGroup>().padding.left = middle / 2 - 250;
        UserPanelRef.ShowScrollView();

        Button closeButton = transform.Find("SharedPhoto/X").GetComponent<Button>();
        closeButton.onClick.RemoveAllListeners();

        closeButton.onClick.AddListener(() =>
        {
            closeButton.onClick.RemoveAllListeners();
            SoundManager.GetInstance().PlaySingle("SoundFX/pop_drip");
            ShrinkVideo();
            CloseShareScreen();
        });

        transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Lkj;lksjdfl;kjsdf");
            transform.Find("CloseButton").GetComponent<Button>().onClick.RemoveAllListeners();
            SoundManager.GetInstance().PlaySingle("SoundFX/pop_drip");
            ShrinkVideo();
            CloseShareScreen();
        });
        
    }

    IEnumerator IEMoveAction;

    // Show is at y = 110
    // Hidden is at y = 850
    public void MoveAction(int move)
    {
        if (IEMoveAction == null)
        {
            IEMoveAction = coMoveAction(move);

            StartCoroutine(IEMoveAction);
        }
    }

    private IEnumerator coMoveAction(int move)
    {
        RectTransform content = transform.Find("SharedPhoto/Albums/Viewport/Content").GetComponent<RectTransform>();

        float speed = 2000;
        Vector2 target = new Vector2(content.anchoredPosition.x + move, content.anchoredPosition.y);

        while (content.anchoredPosition.x != content.anchoredPosition.x + move)
        {
            float step = speed * Time.deltaTime;
            content.anchoredPosition = Vector2.MoveTowards(content.anchoredPosition, target, step);
            yield return null;
        }

        IEMoveAction = null;
    }

    private void CloseShareScreen()
    {
        transform.Find("SharedPhoto/X").gameObject.SetActive(false);
        transform.Find("SharedPhoto/Albums").gameObject.SetActive(false);
        transform.Find("SharedPhoto/DragSlot").gameObject.SetActive(true);
        transform.Find("CloseButton").gameObject.SetActive(false);
    }

    IEnumerator IEShrinkVideo;
    bool VideoFullScreen = true;

    public void ShrinkVideo()
    {
        if (IEShrinkVideo != null)
        {
            StopCoroutine(IEShrinkVideo);
        }
        IEShrinkVideo = null;
        IEShrinkVideo = coShrinkVideo();

        StartCoroutine(IEShrinkVideo);
    }

    public IEnumerator coShrinkVideo()
    {
        Vector2 moveTo = new Vector2(50, -50);
        Vector2 scaleTo = new Vector2(175, 200);
        Vector2 bgScaleTo = new Vector3(.25f, .25f);
        if (!VideoFullScreen)
        {
            moveTo = new Vector2(0, 0);
            scaleTo = new Vector2(710, 1136);
        }

        VideoFullScreen = !VideoFullScreen;

        float speed = 10;

        while (CalleeCanvasZoom.anchoredPosition != moveTo)
        {
            float step = speed * Time.deltaTime;
            CalleeCanvasZoom.anchoredPosition = Vector2.Lerp(CalleeCanvasZoom.anchoredPosition, moveTo, step);
            CalleeCanvasZoom.sizeDelta = Vector2.Lerp(CalleeCanvasZoom.sizeDelta, scaleTo, step);
            calleeCanvas.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(CalleeCanvasZoom.sizeDelta, scaleTo, step);
            yield return null;
        }
    }

    float TotalTime = 0f;

    public IEnumerator CallTimer()
    {
        int hours = 0;
        int minutes = 0;
        float seconds = 0;

        string hourString = "00";
        string minuteString = "00";
        string secondString = "00";

        while (true)
        {
            if (seconds > 60)
            {
                seconds = 0;
                minutes++;
            }

            if (minutes > 59)
            {
                minutes = 0;
                hours++;
            }

            if (minutes < 10)
            {
                minuteString = "0" + minutes;
            }
            else
            {
                minuteString = minutes.ToString();
            }

            if (hours < 10)
            {
                hourString = "0" + hours.ToString();
            }
            else
            {
                hourString = hours.ToString();
            }

            if (seconds < 10)
            {
                secondString = "0" + seconds.ToString("N0");
            }
            else
            {
                secondString = seconds.ToString("N0");
            }

            seconds += Time.deltaTime;

            CallTime.text = hourString + ":" + minuteString + ":" + secondString;
            yield return null;
        }
    }

    void Update()
    {
        if (null != VideoChat.networkTexture)
        {
            UnityEngine.Debug.Log(VideoChat.networkTexture.width + " , " + VideoChat.networkTexture.height);
            calleeCanvas.SetTexture(VideoChat.networkTexture);
            CalleeCanvasZoom.transform.Find("Callee").gameObject.SetActive(false);
        }
    }
}
