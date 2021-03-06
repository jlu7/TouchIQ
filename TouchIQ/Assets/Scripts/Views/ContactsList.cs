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
    List<GameObject> UserBubbleList;
    List<ContactModel> model;

    IEnumerator OnlyOneAnimation;


    

    public void Initialize()
    {
        LoadData();

        MainPanel = this.GetComponent<RectTransform>().Find("Image").GetComponent<RectTransform>();
        BubbleList = new List<GameObject>();
        UserBubbleList = new List<GameObject>();

        GameObject popsicle = UICreate.InstantiateRectTransformPrefab(Resources.Load<GameObject>("Prefabs/ContactsScreen/Popsicle"), MainPanel.GetComponent<RectTransform>());
        _Popsicle = popsicle.GetComponent<Popsicle>();

        GameObject prefab = Resources.Load<GameObject>("Prefabs/FrontPageButtons/MiddleDial");
        MiddleDial = UICreate.InstantiateRectTransformPrefab(prefab, MainPanel.GetComponent<RectTransform>());
        MiddleDial.GetComponent<RectTransform>().localScale = new Vector3(1.75f, 1.75f, 1);

        Button callButton = MiddleDial.transform.Find("CallButton").GetComponent<Button>();
        callButton.onClick.AddListener(() =>
        {
            SoundManager.GetInstance().PlaySingle("SoundFX/pop_drip");
            SoundManager.GetInstance().PlaySingle("SoundFX/Ringing_Phone", true);
            if (UserDataController.GetInstance().ActiveUserType == UserDataController.UserType.Caregiver)
            {
                UserDataController.GetInstance().UserName = "Linda";
                UserDataController.GetInstance().UserImage = "Linda";
            }
            else
            {
                UserDataController.GetInstance().UserName = "Emma";
                UserDataController.GetInstance().UserImage = "Emma";
            }

            TransitionToCall();
            callButton.onClick.RemoveAllListeners();
        });

        if (UserDataController.GetInstance().ActiveUserType == UserDataController.UserType.Caregiver)
        {
            AddBubbleToList(0, 0, model[0]);
            AddBubbleToList(-25, 25, model[1]);
            AddBubbleToList(25, -25, model[2]);
            AddContactListBubble(50, -50);
            AddContactAddBubble(-50, 50);
        }
        else
        {
            AddBubbleToList(0, 0, model[0]);
            AddBubbleToList(-25, 25, model[1]);
            AddBubbleToList(-50, 50, model[2]);
            AddBubbleToList(50, -50, model[3]);
            AddBubbleToList(25, -25, model[4]);
            AddContactListBubble(75, -75);
            AddContactAddBubble(-75, 75);
        }

        //FillWithData();
        //Image img = BubbleList[2].transform.Find("Circle/ImageMask/Image").GetComponent<Image>();
        //RotateArrow(BubbleList[2].transform.Find("Circle").GetComponent<RectTransform>().localRotation, img.sprite, model[2]);

        if (BubbleList.Count > 0)
        {
            BubbleList[0].GetComponent<Button>().onClick.Invoke();
        }

        EnterAnimation();
    }

    private void AddContactListBubble(int rotationValue, int posValue)
    {
        GameObject ContactList = UICreate.InstantiateRectTransformPrefab(Resources.Load<GameObject>("Prefabs/FrontPageButtons/ContactList"), MiddleDial.GetComponent<RectTransform>());
        BubbleList.Add(ContactList);
        ContactList.GetComponent<RectTransform>().SetAsFirstSibling();

        ContactList.GetComponent<RectTransform>().SetParent(MiddleDial.transform);
        ContactList.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        ContactList.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
        ContactList.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 0);
        ContactList.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, rotationValue);
        ContactList.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, posValue);

        
    }

    private void AddContactAddBubble(int rotationValue, int posValue)
    {
        GameObject ContactAdd = UICreate.InstantiateRectTransformPrefab(Resources.Load<GameObject>("Prefabs/FrontPageButtons/ContactAdd"), MiddleDial.GetComponent<RectTransform>());
        BubbleList.Add(ContactAdd);
        ContactAdd.GetComponent<RectTransform>().SetAsFirstSibling();

        ContactAdd.GetComponent<RectTransform>().SetParent(MiddleDial.transform);
        ContactAdd.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        ContactAdd.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
        ContactAdd.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 0);
        ContactAdd.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, rotationValue);
        ContactAdd.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, posValue);

        
    }

    private void AddBubbleToList(int rotationValue, int posValue, ContactModel contactMod)
    {
        GameObject bubble = Resources.Load<GameObject>("Prefabs/FrontPageButtons/Bubble");
        GameObject Test = UICreate.InstantiateRectTransformPrefab(bubble, MiddleDial.GetComponent<RectTransform>());
        Image testImage0 = Test.transform.Find("Circle/ImageMask/Image").GetComponent<Image>();
        testImage0.sprite = Resources.Load<Sprite>("Textures/Photos/Profile/Emma");
        BubbleList.Add(Test);
        UserBubbleList.Add(Test);

        Test.GetComponent<RectTransform>().SetParent(MiddleDial.transform);
        Test.GetComponent<RectTransform>().SetAsFirstSibling();

        Test.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Test.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, 1);
        Test.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 0);
        Test.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, rotationValue);
        Test.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, posValue);
        Test.AddComponent<ContactTag>().Initialize(contactMod, RotateArrow);

        Image img = Test.transform.Find("Circle/ImageMask/Image").GetComponent<Image>();
        img.sprite = Resources.Load<Sprite>("Textures/Photos/Profile/" + contactMod.Image);
        Image fillingBar = Test.transform.Find("Circle/FillingBar").GetComponent<Image>();
        fillingBar.fillAmount = (float)contactMod.Availability / 5f;
        fillingBar.color = availabilityColors[contactMod.Availability];
        //fillingBar.Rebuild(CanvasUpdate.PreRender);
        UnityEngine.Debug.Log(fillingBar.color.r);
        /*Test.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.GetInstance().PlaySingle("SoundFX/music_marimba_chord");
            //RotateArrow(Test.transform.Find("Circle").GetComponent<RectTransform>().localRotation, testImage0.sprite, Test.GetComponent<ContactTag>().TagData);
            RotateArrow(Test.transform.Find("Circle").GetComponent<RectTransform>().localRotation, Test);
            UserDataController.GetInstance().CalleeUserName = Test.GetComponent<ContactTag>().TagData.Name;
            UserDataController.GetInstance().CalleeImage = Test.GetComponent<ContactTag>().TagData.Image;
        });*/
    }

    void LoadData()
    {
        if(UserDataController.GetInstance().ActiveUserType == UserDataController.UserType.Caregiver)
        {
            model = UserDataController.GetInstance().ContactsUsers.Caregiver.Contacts;
        }else
        {
            model = UserDataController.GetInstance().ContactsUsers.Senior.Contacts;
        }
        
    }

    /*void FillWithData()
    {
        List<ContactModel> contactModels = null;
        int userCount = 0;
        if (UserDataController.GetInstance().ActiveUserType == UserDataController.UserType.Caregiver)
        {
            contactModels = UserDataController.GetInstance().ContactsUsers.Caregiver.Contacts;
            userCount = 1;
        }
        else
        {
            contactModels = UserDataController.GetInstance().ContactsUsers.Senior.Contacts;
            userCount = 5;
        }

        for (int i = 0; i < userCount; i++)
        {
            GameObject contactObject = UserBubbleList[i];
            ContactModel contactModel = contactModels[i];

            ContactTag tag = contactObject.AddComponent<ContactTag>();
            tag.Initialize(contactModel, RotateArrow);


            Image img = contactObject.transform.Find("Circle/ImageMask/Image").GetComponent<Image>();
            img.sprite = Resources.Load<Sprite>("Textures/Photos/Profile/" + contactModel.Image);
            Image fillingBar = contactObject.transform.Find("Circle/FillingBar").GetComponent<Image>();
            fillingBar.fillAmount = (float)contactModel.Availability / 5f;
            fillingBar.color = availabilityColors[contactModel.Availability];
            //fillingBar.Rebuild(CanvasUpdate.PreRender);
            UnityEngine.Debug.Log(fillingBar.color.r);
        }
    }*/

    private Dictionary<int, Color> availabilityColors = new Dictionary<int, Color>()
    {
        {1,new Color(239f/255f,43f/255f,64f/255f) },
        {2,new Color(255f/255f,169f/255f,64f/255f) },
        {3,new Color(255f/255f,210f/255f,25f/255f) },
        {4,new Color(145f/255f,229f/255f,48f/255f) },
        {5,new Color(3f/255f,149f/255f,43f/255f) }

    };

    private IEnumerator coRotateArrowIE = null;

    private void RotateArrow(GameObject bubble)
    {
        _Popsicle.SetUserPopsicleInfo(bubble.GetComponent<ContactTag>().Model);
        UnityEngine.Debug.Log("1. ContactsList contactmodel: " + bubble.GetComponent<ContactTag>().Model.Name);
        if (coRotateArrowIE != null)
        {
            StopCoroutine(coRotateArrowIE);
        }
        coRotateArrowIE = null;
        coRotateArrowIE = coRotateArrow(bubble);

        StartCoroutine(coRotateArrowIE);
    }

    private IEnumerator coRotateArrow(GameObject bubble)
    {
        Quaternion rotateTo = bubble.transform.Find("Circle").GetComponent<RectTransform>().localRotation;

        MiddleDial.transform.Find("ImageMask/Image").GetComponent<Image>().sprite = bubble.transform.Find("Circle/ImageMask/Image").GetComponent<Image>().sprite;
        MiddleDial.transform.Find("FillingBar").GetComponent<Image>().fillAmount = (float)bubble.GetComponent<ContactTag>().Model.Availability / 5f;
        MiddleDial.transform.Find("FillingBar").GetComponent<Image>().color = availabilityColors[bubble.GetComponent<ContactTag>().Model.Availability];

        UnityEngine.Debug.Log("2. ContactsList bubble position: " + bubble.transform.localPosition);
        UnityEngine.Debug.Log("3. ContactsList contactmodel: " + bubble.GetComponent<ContactTag>().Model.Name);

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
        UnityEngine.Debug.Log("4. ContactsList contactmodel: " + bubble.GetComponent<ContactTag>().Model.Name);
        
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
    //public ContactModel TagData;
    public ContactModel Model;

    private System.Action<GameObject> rotateAction;

    public void Initialize(ContactModel contactModel, System.Action<GameObject> rotateAction)
    {
        Model = contactModel;
        this.rotateAction = rotateAction;

        this.GetComponent<Button>().onClick.AddListener(ClickHandler);
    }

    void ClickHandler()
    {
        SoundManager.GetInstance().PlaySingle("SoundFX/music_marimba_chord");
        //RotateArrow(Test.transform.Find("Circle").GetComponent<RectTransform>().localRotation, testImage0.sprite, Test.GetComponent<ContactTag>().TagData);
        rotateAction(this.gameObject);
        UserDataController.GetInstance().CalleeUserName = Model.Name;
        UserDataController.GetInstance().CalleeImage = Model.Image;
    }
}
