using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactsList : MonoBehaviour
{
    RectTransform MainPanel = null;

	private void Start ()
    {
        GameObject MiddleDial = Instantiate(Resources.Load<GameObject>("2.0/FrontPageButtons/MiddleDial")) as GameObject;

        MainPanel = this.GetComponent<RectTransform>().Find("Panel").GetComponent<RectTransform>();
        MiddleDial.GetComponent<RectTransform>().SetParent(MainPanel.transform);

        MiddleDial.GetComponent<RectTransform>().localPosition = new Vector3(0, 100, 0);
        MiddleDial.GetComponent<RectTransform>().localScale = new Vector3(2, 2, 1);




        GameObject Test = Instantiate(Resources.Load<GameObject>("2.0/FrontPageButtons/MiddleDial")) as GameObject;

        Test.GetComponent<RectTransform>().SetParent(MiddleDial.transform);

        Test.GetComponent<RectTransform>().localPosition = new Vector3(0, 180, 0);
        Test.GetComponent<RectTransform>().localScale = new Vector3(.35f, .35f, 1);
    }
}
