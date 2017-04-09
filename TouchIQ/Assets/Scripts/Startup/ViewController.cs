using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections;
using Object = UnityEngine.Object;

public class ViewController : MonoBehaviour 
{
    public GameObject CurrentView;

    private static ViewController VC;
    private static Stack<Transform> Views;
    private Transform AnchorRef;

    public static ViewController GetInstance()
    {
        if (VC == null)
        {
            VC = new GameObject("ViewController").AddComponent<ViewController>();
        }
        return VC;
    }

    public void Initialize(Transform anchorRef)
    {
        // Create The FrontPage
        Views = new Stack<Transform>();
        AnchorRef = anchorRef;
        //Views.Push(AnchorRef);
        //GameObject FrontPage = Instantiate(Resources.Load<GameObject>("Prefabs/ContactsScreen/ContactsScreen")) as GameObject;
        GameObject FrontPage = CreateView("Prefabs/ContactsScreen/ContactsScreen");
        //FrontPage.transform.SetParent(AnchorRef, false);
        //Views.Push(FrontPage.transform);
    }

    public GameObject CreateView(string ViewLocation)
    {
        PushView(Instantiate(Resources.Load<GameObject>(ViewLocation)) as GameObject);
        return Views.Peek().gameObject;
    }

    public void PushView(GameObject NewView)
    {
        if(Views.Count > 0)
        {
            Destroy(Views.Pop().gameObject);
        }
        
        NewView.transform.parent = AnchorRef;
        NewView.GetComponent<RectTransform>().anchorMax = new Vector2(.5f, .5f);
        NewView.GetComponent<RectTransform>().anchorMin = new Vector2(.5f, .5f);
        NewView.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
        NewView.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        Views.Push(NewView.transform);
        CurrentView = NewView;
    }
}
