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
        //Initiate The Singletons
        //Transaction<List<TcgCard>> t = new Transaction<List<TcgCard>>();
        yield return null;
        ViewController.GetInstance().Initialize(ViewAnchorRef.transform);
    }
}
