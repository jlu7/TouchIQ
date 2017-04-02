using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeniorCall : MonoBehaviour {

    GameObject remoteUserPanel;
    GameObject localUserPanel;

    GameObject videoCall;

    // Use this for initialization
    void Start ()
    {
        remoteUserPanel = transform.Find("Panel").gameObject;
        localUserPanel = transform.Find("UserPanel").gameObject;

        GameObject videoCallPrefab = Resources.Load<GameObject>("Prefabs/SeniorCall/VideoCall");
        videoCall = GameObject.Instantiate<GameObject>(videoCallPrefab);

        StartCoroutine(videoCall.GetComponent<VideoCall>().SetupVideo(OnVideoStart));
    }

    void OnVideoStart()
    {
        remoteUserPanel.SetActive(false);
        localUserPanel.SetActive(false);
    }
}
