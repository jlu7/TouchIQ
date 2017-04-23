using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatController : MonoBehaviour {

    private static CheatController cheatController;

    public static CheatController GetInstance()
    {
        if (cheatController == null)
        {
            cheatController = new GameObject("CheatController").AddComponent<CheatController>();
        }
        return cheatController;
    }

    private GameObject CheatScreen;

    private GameObject DebugCheat;
    private GameObject UserTypeCheat;

    public bool DebugEnabled = false;

    private Transform CanvasTransform;

    public void Initialize(Transform canvasTransform)
    {
        this.CanvasTransform = canvasTransform;
        GameObject cheatPrefab = Resources.Load<GameObject>("Prefabs/CheatScreen");
        CheatScreen = Instantiate(cheatPrefab, canvasTransform);
        //CheatScreen.SetActive(false);
        CheatScreen.GetComponent<RectTransform>().anchorMax = new Vector2(.5f, .5f);
        CheatScreen.GetComponent<RectTransform>().anchorMin = new Vector2(.5f, .5f);
        CheatScreen.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
        CheatScreen.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        DebugCheat = CheatScreen.transform.Find("DebugCheat").gameObject;
        DebugCheat.GetComponent<Button>().onClick.AddListener(ToggleDebug);

        UserTypeCheat = CheatScreen.transform.Find("UserTypeCheat").gameObject;
        UserTypeCheat.GetComponent<Button>().onClick.AddListener(ToggleUserType);
        UserTypeCheat.transform.Find("Text").GetComponent<Text>().text = "UserType : " + UserDataController.GetInstance().ActiveUserType.ToString();

        CheatScreen.GetComponent<Canvas>().enabled = false;
    } 

    void ToggleDebug()
    {
        DebugEnabled = !DebugEnabled;
        DebugCheat.transform.Find("Text").GetComponent<Text>().text = DebugEnabled ? "Debug : Enabled" : "Debug : Disabled";
    }

    void ToggleUserType()
    {
        var udc = UserDataController.GetInstance();
        if(udc.ActiveUserType == UserDataController.UserType.Caregiver)
        {
            udc.SetUserType(UserDataController.UserType.Senior);
        }
        else
        {
            udc.SetUserType(UserDataController.UserType.Caregiver);
        }
        
        UserTypeCheat.transform.Find("Text").GetComponent<Text>().text = "RESTART to see changes";
        UserTypeCheat.GetComponent<Button>().onClick.RemoveListener(ToggleUserType);
    }
    bool acceptingInput = true;
    private void Update()
    {
        if((Input.touchCount == 3 || Input.GetKeyDown(KeyCode.Tab)) && acceptingInput)
        {
            //CheatScreen.SetActive(!CheatScreen.activeSelf);
            CheatScreen.transform.SetAsLastSibling();
            CheatScreen.GetComponent<Canvas>().enabled = !CheatScreen.GetComponent<Canvas>().enabled;
            acceptingInput = false;
            StartCoroutine(WaitThenEnableInput());
        }
    }

    private IEnumerator WaitThenEnableInput()
    {
        yield return new WaitForSeconds(1f);
        acceptingInput = true;
    }
}
