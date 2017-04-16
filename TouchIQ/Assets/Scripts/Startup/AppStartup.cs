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

        ViewController.GetInstance().Initialize(ViewAnchorRef.transform);
        SoundManager.GetInstance();
        yield return null;
        StartCoroutine(NetworkController.GetInstance().Connect());
        SpeechController.GetInstance().Initialize();
        //Initiate The Singletons
        //Transaction<List<TcgCard>> t = new Transaction<List<TcgCard>>();
    }
}
