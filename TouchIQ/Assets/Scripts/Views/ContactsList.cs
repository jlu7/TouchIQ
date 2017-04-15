using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContactsList : MonoBehaviour
{
    RectTransform MainPanel = null;

    GameObject MiddleDial;

	private void Start ()
    {
        MainPanel = this.GetComponent<RectTransform>().Find("Panel").GetComponent<RectTransform>();

        GameObject prefab4 = Resources.Load<GameObject>("Prefabs/ContactsScreen/Popsicle");
        GameObject Test4 = UICreate.InstantiateRectTransformPrefab(prefab4, MainPanel.GetComponent<RectTransform>());

        GameObject prefab = Resources.Load<GameObject>("Prefabs/FrontPageButtons/MiddleDial");
        MiddleDial = UICreate.InstantiateRectTransformPrefab(prefab, MainPanel.GetComponent<RectTransform>());

        Button callButton = MiddleDial.transform.Find("CallButton").GetComponent<Button>();
        callButton.onClick.AddListener(() => 
        {
            SoundManager.GetInstance().PlaySingle("SoundFX/pop_drip");
            ViewController.GetInstance().CreateView("Prefabs/SeniorCall/SeniorCall");
        });

        GameObject bubble = Resources.Load<GameObject>("Prefabs/FrontPageButtons/Bubble");
        GameObject Test = UICreate.InstantiateRectTransformPrefab(bubble, MiddleDial.GetComponent<RectTransform>());
        Image testImage0 = Test.transform.Find("Circle/ImageMask/Image").GetComponent<Image>();

        Test.GetComponent<RectTransform>().SetParent(MiddleDial.transform);

        Test.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Test.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, 1);
        Test.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 600);
        Test.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -180);
        Test.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 180);
        Test.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.GetInstance().PlaySingle("SoundFX/music_marimba_chord");
            RotateArrow(Test.transform.Find("Circle").GetComponent<RectTransform>().localRotation, testImage0.sprite);
        });

        //StartCoroutine(rotateIn(Vector3.forward * 20, Test.GetComponent<RectTransform>(), 2f));


        GameObject Test1 = UICreate.InstantiateRectTransformPrefab(bubble, MiddleDial.GetComponent<RectTransform>());
        Image testImage1 = Test1.transform.Find("Circle/ImageMask/Image").GetComponent<Image>();
        testImage1.sprite = Resources.Load<Sprite>("Textures/picture0");

        Test1.GetComponent<RectTransform>().SetParent(MiddleDial.transform);

        Test1.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Test1.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, 1);
        Test1.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 600);
        Test1.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 20);
        Test1.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -20);
        Test1.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.GetInstance().PlaySingle("SoundFX/music_marimba_chord");
            RotateArrow(Test1.transform.Find("Circle").GetComponent<RectTransform>().localRotation, testImage1.sprite);
        });

        GameObject Test2 = UICreate.InstantiateRectTransformPrefab(bubble, MiddleDial.GetComponent<RectTransform>());
        Image testImage2 = Test2.transform.Find("Circle/ImageMask/Image").GetComponent<Image>();
        testImage2.sprite = Resources.Load<Sprite>("Textures/picture2");

        Test2.GetComponent<RectTransform>().SetParent(MiddleDial.transform);

        Test2.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Test2.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, 1);
        Test2.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 600);
        Test2.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -60);
        Test2.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 60);
        Test2.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.GetInstance().PlaySingle("SoundFX/music_marimba_chord");
            RotateArrow(Test2.transform.Find("Circle").GetComponent<RectTransform>().localRotation, testImage2.sprite);
        });

        GameObject Test3 = UICreate.InstantiateRectTransformPrefab(bubble, MiddleDial.GetComponent<RectTransform>());
        Image testImage3 = Test3.transform.Find("Circle/ImageMask/Image").GetComponent<Image>();
        testImage3.sprite = Resources.Load<Sprite>("Textures/picture3");

        Test3.GetComponent<RectTransform>().SetParent(MiddleDial.transform);

        Test3.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Test3.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, 1);
        Test3.transform.Find("Circle").GetComponent<RectTransform>().localPosition = new Vector3(0, 600);
        Test3.transform.Find("Circle").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 60);
        Test3.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -60);
        Test3.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.GetInstance().PlaySingle("SoundFX/music_marimba_chord");
            RotateArrow(Test3.transform.Find("Circle").GetComponent<RectTransform>().localRotation, testImage3.sprite);
        });

        RotateArrow(Test3.transform.Find("Circle").GetComponent<RectTransform>().localRotation, testImage3.sprite);
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
}
