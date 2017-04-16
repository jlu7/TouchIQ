using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popsicle : MonoBehaviour
{
    Button ButtonComponent;
    RectTransform View;
    public bool ViewIsVisible = false;
    public bool AcceptingInput = true;

    IEnumerator IEScrollView;

    void Awake()
    {
        ButtonComponent = this.transform.Find("Tab").GetComponent<Button>();
        //ButtonComponent.onClick.AddListener(ShowScrollView);
        View = this.GetComponent<RectTransform>();
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
