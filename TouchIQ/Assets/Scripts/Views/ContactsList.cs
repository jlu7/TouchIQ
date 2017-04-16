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

	private void Awake ()
    {
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
            StartCoroutine(TransitionToCall());
        });

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
        Test.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -0);
        Test.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
        Test.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.GetInstance().PlaySingle("SoundFX/music_marimba_chord");
            RotateArrow(Test.transform.Find("Circle").GetComponent<RectTransform>().localRotation, testImage0.sprite);
        });

        //StartCoroutine(rotateIn(Vector3.forward * 20, Test.GetComponent<RectTransform>(), 2f));


        GameObject Test1 = UICreate.InstantiateRectTransformPrefab(bubble, MiddleDial.GetComponent<RectTransform>());
        Image testImage1 = Test1.transform.Find("Circle/ImageMask/Image").GetComponent<Image>();
        testImage1.sprite = Resources.Load<Sprite>("Textures/Photos/Profile/Angel");
        BubbleList.Add(Test1);

        Test1.GetComponent<RectTransform>().SetParent(MiddleDial.transform);
        Test1.GetComponent<RectTransform>().SetAsFirstSibling();

        Test1.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Test1.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, 1);
        Test1.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 0);
        Test1.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -25);
        Test1.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 25);
        Test1.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.GetInstance().PlaySingle("SoundFX/music_marimba_chord");
            RotateArrow(Test1.transform.Find("Circle").GetComponent<RectTransform>().localRotation, testImage1.sprite);
        });

        GameObject Test2 = UICreate.InstantiateRectTransformPrefab(bubble, MiddleDial.GetComponent<RectTransform>());
        Image testImage2 = Test2.transform.Find("Circle/ImageMask/Image").GetComponent<Image>();
        testImage2.sprite = Resources.Load<Sprite>("Textures/Photos/Profile/Cristina");
        BubbleList.Add(Test2);

        Test2.GetComponent<RectTransform>().SetParent(MiddleDial.transform);
        Test2.GetComponent<RectTransform>().SetAsFirstSibling();

        Test2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Test2.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, 1);
        Test2.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 0);
        Test2.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -50);
        Test2.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 50);
        Test2.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.GetInstance().PlaySingle("SoundFX/music_marimba_chord");
            RotateArrow(Test2.transform.Find("Circle").GetComponent<RectTransform>().localRotation, testImage2.sprite);
        });

        GameObject Test3 = UICreate.InstantiateRectTransformPrefab(bubble, MiddleDial.GetComponent<RectTransform>());
        Image testImage3 = Test3.transform.Find("Circle/ImageMask/Image").GetComponent<Image>();
        testImage3.sprite = Resources.Load<Sprite>("Textures/Photos/Profile/David");
        BubbleList.Add(Test3);

        Test3.GetComponent<RectTransform>().SetParent(MiddleDial.transform);
        Test3.GetComponent<RectTransform>().SetAsFirstSibling();

        Test3.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Test3.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, 1);
        Test3.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 0);
        Test3.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 50);
        Test3.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -50);
        Test3.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.GetInstance().PlaySingle("SoundFX/music_marimba_chord");
            RotateArrow(Test3.transform.Find("Circle").GetComponent<RectTransform>().localRotation, testImage3.sprite);
        });


        GameObject Test4 = UICreate.InstantiateRectTransformPrefab(bubble, MiddleDial.GetComponent<RectTransform>());
        Image testImage4 = Test4.transform.Find("Circle/ImageMask/Image").GetComponent<Image>();
        testImage4.sprite = Resources.Load<Sprite>("Textures/Photos/Profile/Edwin");
        BubbleList.Add(Test4);

        Test4.GetComponent<RectTransform>().SetParent(MiddleDial.transform);
        Test4.GetComponent<RectTransform>().SetAsFirstSibling();

        Test4.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Test4.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, 1);
        Test4.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 0);
        Test4.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 25);
        Test4.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -25);
        Test4.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.GetInstance().PlaySingle("SoundFX/music_marimba_chord");
            RotateArrow(Test4.transform.Find("Circle").GetComponent<RectTransform>().localRotation, testImage4.sprite);
        });

        GameObject ContactList = UICreate.InstantiateRectTransformPrefab(Resources.Load<GameObject>("Prefabs/FrontPageButtons/ContactList"), MiddleDial.GetComponent<RectTransform>());
        BubbleList.Add(ContactList);
        ContactList.GetComponent<RectTransform>().SetAsFirstSibling();

        ContactList.GetComponent<RectTransform>().SetParent(MiddleDial.transform);
        ContactList.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        ContactList.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
        ContactList.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 0);
        ContactList.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -75);
        ContactList.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 75);

        GameObject ContactAdd = UICreate.InstantiateRectTransformPrefab(Resources.Load<GameObject>("Prefabs/FrontPageButtons/ContactAdd"), MiddleDial.GetComponent<RectTransform>());
        BubbleList.Add(ContactAdd);
        ContactAdd.GetComponent<RectTransform>().SetAsFirstSibling();

        ContactAdd.GetComponent<RectTransform>().SetParent(MiddleDial.transform);
        ContactAdd.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        ContactAdd.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
        ContactAdd.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 0);
        ContactAdd.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 75);
        ContactAdd.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -75);

        RotateArrow(Test3.transform.Find("Circle").GetComponent<RectTransform>().localRotation, testImage3.sprite);
    }

    private void Start()
    {
        StartCoroutine(EnterAnimation());
    }

    private IEnumerator coRotateArrowIE = null;

    private void RotateArrow(Quaternion rotateTo, Sprite image)
    {
        if (coRotateArrowIE != null)
        {
            StopCoroutine(coRotateArrowIE);
        }
        coRotateArrowIE = null;
        coRotateArrowIE = coRotateArrow(rotateTo, image);

        StartCoroutine(coRotateArrowIE);
    }

    private IEnumerator coRotateArrow(Quaternion rotateTo, Sprite image)
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
    }

    public IEnumerator IncomingCall()
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

    private IEnumerator TransitionToCall()
    {
        _Popsicle.HideScrollView();

        Image Arrow = MiddleDial.transform.Find("ArrowOrigin/Arrow").GetComponent<Image>();

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

        yield return new WaitForSeconds(5f);

        ViewController.GetInstance().CreateView("Prefabs/SeniorCall/SeniorCall");
    }

    private IEnumerator EnterAnimation()
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
