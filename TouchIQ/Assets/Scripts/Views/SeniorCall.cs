﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeniorCall : MonoBehaviour {

    GameObject remoteUserPanel;
    GameObject localUserPanel;

    GameObject videoCall;

    Button endCall;

    // Use this for initialization
    void Start ()
    {
        remoteUserPanel = transform.Find("Panel").gameObject;
        localUserPanel = transform.Find("UserPanel").gameObject;
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
        transform.Find("SharedPhoto/DragSlot").gameObject.AddComponent<DragSlot>();
    }

    void OnVideoStart()
    {
        remoteUserPanel.SetActive(false);
    }
}
