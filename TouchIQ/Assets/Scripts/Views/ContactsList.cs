using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContactsList : MonoBehaviour
{
    RectTransform MainPanel = null;

	private void Start ()
    {
        MainPanel = this.GetComponent<RectTransform>().Find("Panel").GetComponent<RectTransform>();

        GameObject prefab = Resources.Load<GameObject>("Prefabs/FrontPageButtons/MiddleDial");
        GameObject MiddleDial = UICreate.InstantiateRectTransformPrefab(prefab, MainPanel.GetComponent<RectTransform>());

        Button callButton = MiddleDial.transform.Find("CallButton").GetComponent<Button>();
        callButton.onClick.AddListener(() => 
        {
            ViewController.GetInstance().CreateView("Prefabs/SeniorCall/SeniorCall");
        });

        GameObject bubble = Resources.Load<GameObject>("Prefabs/FrontPageButtons/Bubble");
        GameObject Test = UICreate.InstantiateRectTransformPrefab(bubble, MiddleDial.GetComponent<RectTransform>());

        Test.GetComponent<RectTransform>().SetParent(MiddleDial.transform);

        Test.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Test.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, 1);
        Test.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 600);
        Test.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -20);
        Test.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 20);

        GameObject Test1 = UICreate.InstantiateRectTransformPrefab(bubble, MiddleDial.GetComponent<RectTransform>());

        Test1.GetComponent<RectTransform>().SetParent(MiddleDial.transform);

        Test1.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Test1.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, 1);
        Test1.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 600);
        Test1.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 20);
        Test1.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -20);

        GameObject Test2 = UICreate.InstantiateRectTransformPrefab(bubble, MiddleDial.GetComponent<RectTransform>());

        Test2.GetComponent<RectTransform>().SetParent(MiddleDial.transform);

        Test2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Test2.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, 1);
        Test2.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 600);
        Test2.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -60);
        Test2.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 60);

        GameObject Test3 = UICreate.InstantiateRectTransformPrefab(bubble, MiddleDial.GetComponent<RectTransform>());

        Test3.GetComponent<RectTransform>().SetParent(MiddleDial.transform);

        Test3.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Test3.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, 1);
        Test3.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 600);
        Test3.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 60);
        Test3.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -60);


        GameObject prefab4 = Resources.Load<GameObject>("Prefabs/ContactsScreen/Popsicle");
        GameObject Test4 = UICreate.InstantiateRectTransformPrefab(prefab4, MainPanel.GetComponent<RectTransform>());
    }
}
