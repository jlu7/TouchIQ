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




        GameObject Test = Instantiate(Resources.Load<GameObject>("2.0/FrontPageButtons/Bubble")) as GameObject;

        Test.GetComponent<RectTransform>().SetParent(MiddleDial.transform);

        Test.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Test.GetComponent<RectTransform>().localScale = new Vector3(.35f, .35f, 1);
        Test.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 500);
        Test.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -20);
        Test.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 20);

        GameObject Test1 = Instantiate(Resources.Load<GameObject>("2.0/FrontPageButtons/Bubble")) as GameObject;

        Test1.GetComponent<RectTransform>().SetParent(MiddleDial.transform);

        Test1.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Test1.GetComponent<RectTransform>().localScale = new Vector3(.35f, .35f, 1);
        Test1.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 500);
        Test1.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 20);
        Test1.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -20);

        GameObject Test2 = Instantiate(Resources.Load<GameObject>("2.0/FrontPageButtons/Bubble")) as GameObject;

        Test2.GetComponent<RectTransform>().SetParent(MiddleDial.transform);

        Test2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Test2.GetComponent<RectTransform>().localScale = new Vector3(.35f, .35f, 1);
        Test2.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 500);
        Test2.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -60);
        Test2.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 60);

        GameObject Test3 = Instantiate(Resources.Load<GameObject>("2.0/FrontPageButtons/Bubble")) as GameObject;

        Test3.GetComponent<RectTransform>().SetParent(MiddleDial.transform);

        Test3.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Test3.GetComponent<RectTransform>().localScale = new Vector3(.35f, .35f, 1);
        Test3.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 500);
        Test3.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 60);
        Test3.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -60);
    }
}
