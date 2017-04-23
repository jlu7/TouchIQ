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
        UserDataController.GetInstance().Initialize();
        while(UserDataController.GetInstance().ContactsUsers == null)
        {
            yield return null;
        }
        
        SoundManager.GetInstance();
        yield return new WaitForSeconds(.1f);
        StartCoroutine(NetworkController.GetInstance().Connect());
        SpeechController.GetInstance().Initialize();
        PhotoController.GetInstance().Initialize();

        

        CheatController.GetInstance().Initialize(ViewAnchorRef.transform);

        ContactsList cl = ViewController.GetInstance().CurrentView.GetComponent<ContactsList>();
        cl.Initialize();
        //Initiate The Singletons
        //Transaction<List<TcgCard>> t = new Transaction<List<TcgCard>>();
    }
}
