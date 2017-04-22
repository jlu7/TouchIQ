using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContactsList : MonoBehaviour
{
    public enum CurrentState
    {
        ContactList,
        Calling,
        InCall
    }

    RectTransform MainPanel = null;
    CurrentState _State = CurrentState.ContactList;

    GameObject MiddleDial;
    Popsicle _Popsicle;
    List<GameObject> BubbleList;
    ContactsUsersModel model;

    IEnumerator OnlyOneAnimation;


    private void Awake ()
    {
        LoadData();

        MainPanel = this.GetComponent<RectTransform>().Find("Image").GetComponent<RectTransform>();
        BubbleList = new List<GameObject>();

        GameObject popsicle = UICreate.InstantiateRectTransformPrefab(Resources.Load<GameObject>("Prefabs/ContactsScreen/Popsicle"), MainPanel.GetComponent<RectTransform>());
        _Popsicle = popsicle.GetComponent<Popsicle>();

        GameObject prefab = Resources.Load<GameObject>("Prefabs/FrontPageButtons/MiddleDial");
        MiddleDial = UICreate.InstantiateRectTransformPrefab(prefab, MainPanel.GetComponent<RectTransform>());
        MiddleDial.GetComponent<RectTransform>().localScale = new Vector3(1.75f, 1.75f, 1);

        Button callButton = MiddleDial.transform.Find("CallButton").GetComponent<Button>();
        callButton.onClick.AddListener(() => 
        {
            SoundManager.GetInstance().PlaySingle("SoundFX/pop_drip");
            TransitionToCall();
        });

        AddBubbleToList(0,0);
        AddBubbleToList(-25, 25);
        AddBubbleToList(-50, 50);
        AddBubbleToList(50, -50);
        AddBubbleToList(25, -25);

        GameObject ContactList = UICreate.InstantiateRectTransformPrefab(Resources.Load<GameObject>("Prefabs/FrontPageButtons/ContactList"), MiddleDial.GetComponent<RectTransform>());
        BubbleList.Add(ContactList);
        ContactList.GetComponent<RectTransform>().SetAsFirstSibling();

        ContactList.GetComponent<RectTransform>().SetParent(MiddleDial.transform);
        ContactList.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        ContactList.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
        ContactList.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 0);
        ContactList.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 75);
        ContactList.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -75);

        GameObject ContactAdd = UICreate.InstantiateRectTransformPrefab(Resources.Load<GameObject>("Prefabs/FrontPageButtons/ContactAdd"), MiddleDial.GetComponent<RectTransform>());
        BubbleList.Add(ContactAdd);
        ContactAdd.GetComponent<RectTransform>().SetAsFirstSibling();

        ContactAdd.GetComponent<RectTransform>().SetParent(MiddleDial.transform);
        ContactAdd.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        ContactAdd.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
        ContactAdd.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 0);
        ContactAdd.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -75);
        ContactAdd.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 75);

        FillWithData();
        Image img = BubbleList[2].transform.Find("Circle/ImageMask/Image").GetComponent<Image>();
        RotateArrow(BubbleList[2].transform.Find("Circle").GetComponent<RectTransform>().localRotation, img.sprite, 2);
    }

    public void AddBubbleToList(int rotationValue, int posValue)
    {
        GameObject bubble = Resources.Load<GameObject>("Prefabs/FrontPageButtons/Bubble");
        GameObject Test = UICreate.InstantiateRectTransformPrefab(bubble, MiddleDial.GetComponent<RectTransform>());
        Image testImage0 = Test.transform.Find("Circle/ImageMask/Image").GetComponent<Image>();
        testImage0.sprite = Resources.Load<Sprite>("Textures/Photos/Profile/Emma");
        BubbleList.Add(Test);

        Test.GetComponent<RectTransform>().SetParent(MiddleDial.transform);
        Test.GetComponent<RectTransform>().SetAsFirstSibling();

        Test.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Test.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, 1);
        Test.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 0);
        Test.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, rotationValue);
        Test.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, posValue);
        Test.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.GetInstance().PlaySingle("SoundFX/music_marimba_chord");
            RotateArrow(Test.transform.Find("Circle").GetComponent<RectTransform>().localRotation, testImage0.sprite, Test.GetComponent<ContactTag>().TagData);
        });
    }

    private void Start()
    {
        EnterAnimation();
    }

    void LoadData()
    {
        model = UserDataController.GetInstance().ContactsUsers;
    }

    void FillWithData()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject contactObject = BubbleList[i];
            ContactModel contactModel = model.Caregiver.Contacts[i];

            contactObject.AddComponent<ContactTag>().TagData = i;

            Image img = contactObject.transform.Find("Circle/ImageMask/Image").GetComponent<Image>();
            img.sprite = Resources.Load<Sprite>("Textures/Photos/Profile/" + contactModel.Image);
        }
    }

    private IEnumerator coRotateArrowIE = null;

    private void RotateArrow(Quaternion rotateTo, Sprite image, int tagData)
    {
        if (coRotateArrowIE != null)
        {
            StopCoroutine(coRotateArrowIE);
        }
        coRotateArrowIE = null;
        coRotateArrowIE = coRotateArrow(rotateTo, image, tagData);

        StartCoroutine(coRotateArrowIE);
    }

    private IEnumerator coRotateArrow(Quaternion rotateTo, Sprite image, int tagData)
    {
        MiddleDial.transform.Find("ImageMask/Image").GetComponent<Image>().sprite = image;

        float speed = 500;
        while (MiddleDial.transform.Find("ArrowOrigin").GetComponent<RectTransform>().localRotation.z != -rotateTo.z)
        {
            float step = speed * Time.deltaTime;

            MiddleDial.transform.Find("ArrowOrigin").GetComponent<RectTransform>().localRotation = 
                Quaternion.RotateTowards(
                MiddleDial.transform.Find("ArrowOrigin").GetComponent<RectTransform>().localRotation,
                new Quaternion(rotateTo.x, rotateTo.y, -rotateTo.z, rotateTo.w), step);
            yield return null;
        }
        _Popsicle.SetUserPopsicleInfo(model.Caregiver.Contacts[tagData]);
    }

    public void IncomingCall()
    {
        if (OnlyOneAnimation != null)
        {
            StopCoroutine(OnlyOneAnimation);
        }

        OnlyOneAnimation = null;
        OnlyOneAnimation = coIncomingCall();
        StartCoroutine(OnlyOneAnimation);
    }

    private IEnumerator coIncomingCall()
    {
        if (_Popsicle.ViewIsVisible)
        {
            _Popsicle.ShowScrollView();
        }
        Image Arrow = MiddleDial.transform.Find("ArrowOrigin/Arrow").GetComponent<Image>();

        float timeToReachTarget = 2f;

        var t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime / timeToReachTarget;
            MiddleDial.transform.localScale = Vector3.Lerp(MiddleDial.transform.localScale, new Vector3(1.75f, 1.75f), t);
            Arrow.color = Color.Lerp(Arrow.color, Color.clear, t);
            foreach (GameObject bubble in BubbleList)
            {
                bubble.transform.Find("Circle").localPosition = Vector3.Lerp(bubble.transform.Find("Circle").localPosition, new Vector3(0, 0, 0), t);
            }
            yield return null;
        }

        ViewController.GetInstance().CreateView("Prefabs/SeniorCall/IncomingCallScreen");
    }

    public void TransitionToCall()
    {
        if (OnlyOneAnimation != null)
        {
            StopCoroutine(OnlyOneAnimation);
        }

        OnlyOneAnimation = null;
        OnlyOneAnimation = coTransitionToCall();
        StartCoroutine(OnlyOneAnimation);
    }

    private IEnumerator coTransitionToCall()
    {
        _Popsicle.HideScrollView();

        Image Arrow = MiddleDial.transform.Find("ArrowOrigin/Arrow").GetComponent<Image>();
        StartCoroutine(RotateDotRings());

        float timeToReachTarget = 2f;

        float t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime / timeToReachTarget;

            MiddleDial.transform.localScale = Vector3.Lerp(MiddleDial.transform.localScale, new Vector3(1.75f, 1.75f), t);
            Arrow.color = Color.Lerp(Arrow.color, Color.clear, t);
            foreach (GameObject bubble in BubbleList)
            {
                bubble.transform.Find("Circle").localPosition = Vector3.Lerp(bubble.transform.Find("Circle").localPosition, new Vector3(0, 0, 0), t);
            }
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        ViewController.GetInstance().CreateView("Prefabs/SeniorCall/SeniorCall");
    }

    protected IEnumerator RotateDotRings()
    {
        RectTransform DotRing = MiddleDial.transform.Find("DotRing").GetComponent<RectTransform>();

        float timeToReachTarget = 2f;

        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToReachTarget;

            DotRing.transform.localScale = Vector3.Lerp(DotRing.transform.localScale, new Vector3(1.25f, 1.25f), t);
            yield return null;
        }

        while (true)
        {
            DotRing.Rotate(Vector3.forward * Time.deltaTime * 4);
            yield return null;
        }
    }

    private void EnterAnimation()
    {
        if (OnlyOneAnimation != null)
        {
            StopCoroutine(OnlyOneAnimation);
        }

        OnlyOneAnimation = null;
        OnlyOneAnimation = coEnterAnimation();
        StartCoroutine(OnlyOneAnimation);
    }

    private IEnumerator coEnterAnimation()
    {
        yield return new WaitForSeconds(1f);
        _Popsicle.ShowScrollView();
        Image Arrow = MiddleDial.transform.Find("ArrowOrigin/Arrow").GetComponent<Image>();
        Image CallButton = MiddleDial.transform.Find("CallButton").GetComponent<Image>();

        float timeToReachTarget = 2f;

        float t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime / timeToReachTarget;

            MiddleDial.transform.localScale = Vector3.Lerp(MiddleDial.transform.localScale, new Vector3(1f, 1f), t);
            Arrow.color = Color.Lerp(Arrow.color, Color.white, t);
            CallButton.color = Color.Lerp(CallButton.color, Color.white, t);
            foreach (GameObject bubble in BubbleList)
            {
                if (bubble.name == "Bubble(Clone)")
                {
                    bubble.transform.Find("Circle").localPosition = Vector3.Lerp(bubble.transform.Find("Circle").localPosition, new Vector3(0, 600, 0), t);
                }
                else
                {
                    bubble.transform.Find("Circle").localPosition = Vector3.Lerp(bubble.transform.Find("Circle").localPosition, new Vector3(0, 260, 0), t);
                }
            }
            yield return null;
        }
    }
}

public class ContactTag : MonoBehaviour
{
    public int TagData;
}
