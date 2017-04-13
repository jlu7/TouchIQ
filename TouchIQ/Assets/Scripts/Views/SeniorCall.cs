using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeniorCall : MonoBehaviour {

    GameObject remoteUserPanel;
    GameObject localUserPanel;
    UserPanel UserPanelRef;

    GameObject videoCall;

    Button endCall;

    // Use this for initialization
    void Start ()
    {
        remoteUserPanel = transform.Find("Panel").gameObject;
        localUserPanel = transform.Find("UserPanel").gameObject;
        UserPanelRef = localUserPanel.AddComponent<UserPanel>();
        endCall = transform.Find("BottomBar/CallButton").GetComponent<Button>();
        endCall.onClick.AddListener(() =>
        {
            videoCall.GetComponent<VideoCall>().Restart();
        });

        GameObject videoCallPrefab = Resources.Load<GameObject>("Prefabs/SeniorCall/VideoCall");
        videoCall = GameObject.Instantiate<GameObject>(videoCallPrefab);
        videoCall.transform.SetParent(this.transform);
        videoCall.transform.localPosition = Vector3.zero;

        StartCoroutine(videoCall.GetComponent<VideoCall>().SetupVideo(OnVideoStart));
        DragSlot dragComponent = transform.Find("SharedPhoto/DragSlot").gameObject.AddComponent<DragSlot>();
        dragComponent.method += SharedPhotoViewOn;
        NetworkController.GetInstance().OnPhotoReceived += ReceivedNetworkPhoto;
    }

    void OnDestroy()
    {
        NetworkController.GetInstance().OnPhotoReceived -= ReceivedNetworkPhoto;
    }

    void OnVideoStart()
    {
        remoteUserPanel.SetActive(false);
    }

    private void ReceivedNetworkPhoto(string photoName)
    {
        Sprite spr = Resources.Load<Sprite>("Textures/" + photoName);
        SharedPhotoViewOn(spr);
    }

    private void SharedPhotoViewOn(Sprite spr)
    {
        transform.Find("SharedPhoto/X").gameObject.SetActive(true);
        transform.Find("SharedPhoto/Albums").gameObject.SetActive(true);
        transform.Find("SharedPhoto/DragSlot").gameObject.SetActive(false);
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
            CloseShareScreen();
        });

        NetworkController.GetInstance().SendPhotoMessage(spr.name);

        /*        Button Right = transform.Find("SharedPhoto/Albums/RightButton").GetComponent<Button>();
                Right.onClick.RemoveAllListeners();

                Right.onClick.AddListener(() =>
                {
                    MoveAction(-550);
                });

                Button Left = transform.Find("SharedPhoto/Albums/LeftButton").GetComponent<Button>();
                Left.onClick.RemoveAllListeners();

                Left.onClick.AddListener(() =>
                {
                    MoveAction(550);
                });
                */
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
    }
}
