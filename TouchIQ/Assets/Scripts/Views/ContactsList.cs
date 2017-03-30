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
        MiddleDial.GetComponent<RectTransform>().parent = MainPanel.transform;

        MiddleDial.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        MiddleDial.GetComponent<RectTransform>().localScale = new Vector3(3, 3, 1);
    }
}
