using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool DebugEnabled = false;

    public void Initialize(Transform canvasTransform)
    {
        GameObject cheatPrefab = Resources.Load<GameObject>("Prefabs/CheatScreen");
        CheatScreen = Instantiate(cheatPrefab, canvasTransform);
        //CheatScreen.SetActive(false);
        CheatScreen.GetComponent<RectTransform>().anchorMax = new Vector2(.5f, .5f);
        CheatScreen.GetComponent<RectTransform>().anchorMin = new Vector2(.5f, .5f);
        CheatScreen.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
        CheatScreen.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        CheatScreen.GetComponent<Canvas>().enabled = false;
    } 

    private void Update()
    {
        if(Input.touchCount == 3 || Input.GetKeyDown(KeyCode.Tab))
        {
            //CheatScreen.SetActive(!CheatScreen.activeSelf);
            CheatScreen.GetComponent<Canvas>().enabled = true;
        }
    }
}
