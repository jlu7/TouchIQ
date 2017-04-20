using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Popsicle : MonoBehaviour
{
    Button ButtonComponent;
    RectTransform View;
    public bool ViewIsVisible = false;
    public bool AcceptingInput = true;

    Text NameText;
    Text TimeToCallText;
    Text LastTalkedText;
    Text RequestedOfMeText;
    Text RequestedOfOthersText;

    ScrollRect RequestedOfMeScroll;
    ScrollRect RequestedOfOthersScroll;

    GameObject requestedOfMeItemPrefab;
    GameObject requestedOfOthersItemPrefab;

    IEnumerator IEScrollView;

    void Awake()
    {
        ButtonComponent = this.transform.Find("Tab").GetComponent<Button>();
        //ButtonComponent.onClick.AddListener(ShowScrollView);
        View = this.GetComponent<RectTransform>();

        //Grab refs too all of the custom Text
        NameText = this.transform.Find("Tab/Name").GetComponent<Text>();
        NameText.text = "\nDURR";
        TimeToCallText = this.transform.Find("Tab/TimeToCall").GetComponent<Text>();
        TimeToCallText.text = "DURR";
        LastTalkedText = this.transform.Find("Tab/LastTalked").GetComponent<Text>();
        LastTalkedText.text = "DURR";
        //RequestedOfMeText = this.transform.Find("Requests/RequestedOfMe/RequestedOfMeBG/RequestedOfMeText").GetComponent<Text>();
        //RequestedOfMeText.text = "DURR";
        //RequestedOfOthersText = this.transform.Find("Requests/RequestedOfOthers/RequestedOfOthersBG/RequestedOfMeText").GetComponent<Text>();
        //RequestedOfOthersText.text = "DURR";

        requestedOfMeItemPrefab = Resources.Load<GameObject>("Prefabs/ContactsScreen/RequestedOfMeItem");
        requestedOfOthersItemPrefab = Resources.Load<GameObject>("Prefabs/ContactsScreen/RequestedOfOthersItem");

        RequestedOfMeScroll = this.transform.Find("Requests/RequestedOfMe/Scroll View").GetComponent<ScrollRect>();
        RequestedOfOthersScroll = this.transform.Find("Requests/RequestedOfOthers/Scroll View").GetComponent<ScrollRect>();

        List<LayoutElement> les = RequestedOfMeScroll.content.GetComponent<VerticalLayoutGroup>().GetComponentsInChildren<LayoutElement>().ToList();
        foreach(LayoutElement le in les)
        {
            Destroy(le.gameObject);
        }
        for (int j = 0; j < 5; j++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(requestedOfMeItemPrefab, RequestedOfMeScroll.content.transform);
            go.GetComponent<RectTransform>().localScale = Vector3.one;
            go.transform.Find("Text").GetComponent<Text>().text = "DURR";
        }

        les = RequestedOfOthersScroll.content.GetComponent<VerticalLayoutGroup>().GetComponentsInChildren<LayoutElement>().ToList();
        foreach (LayoutElement le in les)
        {
            Destroy(le.gameObject);
        }
        for (int j = 0; j < 5; j++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(requestedOfMeItemPrefab, RequestedOfOthersScroll.content.transform);
            go.GetComponent<RectTransform>().localScale = Vector3.one;
            go.transform.Find("Text").GetComponent<Text>().text = "DURR";
        }


    }

    private void Start()
    {
        View.anchoredPosition = new Vector2(View.anchoredPosition.x, -406);
    }
    /*
    public void ChangeState(ContactsList.CurrentState State)
    {
        if (State == ContactsList.CurrentState.ContactList)
        {
            AcceptingInput = true;
            transform.Find("Albums").gameObject.SetActive(false);
            transform.Find("Requests").gameObject.SetActive(true);
            transform.Find("Tab/TimeToCall").gameObject.SetActive(true);
            transform.Find("Tab/LastTalked").gameObject.SetActive(true);
            transform.Find("Tab/Calling").gameObject.SetActive(false);
        }

        if (State == ContactsList.CurrentState.Calling)
        {
            AcceptingInput = false;
            transform.Find("Albums").gameObject.SetActive(false);
            transform.Find("Requests").gameObject.SetActive(false);
            transform.Find("Tab/TimeToCall").gameObject.SetActive(false);
            transform.Find("Tab/LastTalked").gameObject.SetActive(false);
            transform.Find("Tab/Calling").gameObject.SetActive(true);
        }

        if (State == ContactsList.CurrentState.Calling)
        {
            AcceptingInput = false;
            transform.Find("Albums").gameObject.SetActive(false);
            transform.Find("Requests").gameObject.SetActive(false);
            transform.Find("Tab/TimeToCall").gameObject.SetActive(false);
            transform.Find("Tab/LastTalked").gameObject.SetActive(false);
            transform.Find("Tab/Calling").gameObject.SetActive(true);
        }
    }
    */

    public void SetUserPopsicleInfo(ContactModel contactModel)
    {
        NameText.text = contactModel.Name;
        TimeToCallText.text = contactModel.TimeToCall;
        LastTalkedText.text = contactModel.LastTalked;

        List<LayoutElement> les = RequestedOfMeScroll.content.GetComponent<VerticalLayoutGroup>().GetComponentsInChildren<LayoutElement>().ToList();
        foreach (LayoutElement le in les)
        {
            Destroy(le.gameObject);
        }
        foreach(string req in contactModel.RequestedOfMe)
        {
            GameObject go = GameObject.Instantiate<GameObject>(requestedOfMeItemPrefab, RequestedOfMeScroll.content.transform);
            go.GetComponent<RectTransform>().localScale = Vector3.one;
            go.transform.Find("Text").GetComponent<Text>().text = req;
        }

        les = RequestedOfOthersScroll.content.GetComponent<VerticalLayoutGroup>().GetComponentsInChildren<LayoutElement>().ToList();
        foreach (LayoutElement le in les)
        {
            Destroy(le.gameObject);
        }
        foreach (string req in contactModel.RequestedOfOthers)
        {
            GameObject go = GameObject.Instantiate<GameObject>(requestedOfOthersItemPrefab, RequestedOfOthersScroll.content.transform);
            go.GetComponent<RectTransform>().localScale = Vector3.one;
            go.transform.Find("Text").GetComponent<Text>().text = "DURR";
        }
    }

    // Show is at y = 0
    // Hidden is at y = -406
    public void ShowScrollView()
    {
        if (IEScrollView != null)
        {
            StopCoroutine(IEScrollView);
        }
        IEScrollView = null;
        IEScrollView = coShowScrollView();

        StartCoroutine(IEScrollView);
    }

    public void HideScrollView()
    {
        if (IEScrollView != null)
        {
            StopCoroutine(IEScrollView);
        }
        IEScrollView = null;
        IEScrollView = coHideScrollView();

        StartCoroutine(IEScrollView);
    }

    protected IEnumerator coShowScrollView()
    {
        int y = 0;

        transform.Find("Tab/TimeToCall").gameObject.SetActive(true);
        transform.Find("Tab/LastTalked").gameObject.SetActive(true);

        ViewIsVisible = !ViewIsVisible;
        Debug.Log("SHOW");

        float speed = 2000;
        Vector2 target = new Vector2(View.anchoredPosition.x, y);
        Vector2 NameTarget = new Vector2(0, -4);

        while (View.anchoredPosition.y != y)
        {
            float step = speed * Time.deltaTime;
            transform.Find("Tab/Name").localPosition = Vector2.MoveTowards(transform.Find("Tab/Name").localPosition, NameTarget, step);
            View.anchoredPosition = Vector2.MoveTowards(View.anchoredPosition, target, step);
            yield return null;
        }
    }

    protected IEnumerator coHideScrollView()
    {
        int y = -406;
        transform.Find("Tab/TimeToCall").gameObject.SetActive(false);
        transform.Find("Tab/LastTalked").gameObject.SetActive(false);

        Debug.Log("HIDE");

        ViewIsVisible = !ViewIsVisible;

        float speed = 2000;
        Vector2 target = new Vector2(View.anchoredPosition.x, y);
        Vector2 NameTarget = new Vector2(0, 70);

        while (View.anchoredPosition.y != y)
        {
            float step = speed * Time.deltaTime;
            transform.Find("Tab/Name").localPosition = Vector2.MoveTowards(transform.Find("Tab/Name").localPosition, NameTarget, step);
            View.anchoredPosition = Vector2.MoveTowards(View.anchoredPosition, target, step);
            yield return null;
        }
    }
}
