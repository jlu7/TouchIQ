using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DealFinder.Network.Models;

public class AppStartup : MonoBehaviour
{
    public GameObject ViewAnchorRef;

	// Use this for initialization
	void Start () 
    {
        StartCoroutine(Startup());
	}

    IEnumerator Startup()
    {
        UserDataController.GetInstance().Initialize();
        while(UserDataController.GetInstance().ContactsUsers == null)
        {
            yield return null;
        }
        SoundManager.GetInstance();
        yield return null;
        StartCoroutine(NetworkController.GetInstance().Connect());
        SpeechController.GetInstance().Initialize();
        PhotoController.GetInstance().Initialize();

        ViewController.GetInstance().Initialize(ViewAnchorRef.transform);

        CheatController.GetInstance().Initialize(ViewAnchorRef.transform);
        //Initiate The Singletons
        //Transaction<List<TcgCard>> t = new Transaction<List<TcgCard>>();
    }
}
