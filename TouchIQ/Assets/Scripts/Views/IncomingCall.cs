using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncomingCall : MonoBehaviour
{
    Button acceptButton;
    Button declineButton;
    IEnumerator ieRotateDotRings;

    // Use this for initialization
    void Start ()
    {
        SoundManager.GetInstance().PlaySingle("SoundFX/Ringing_Phone", true);

        ieRotateDotRings = RotateDotRings();
        StartCoroutine(ieRotateDotRings);

        acceptButton = this.transform.Find("Panel/BottomBar/AcceptCall").GetComponent<Button>();
        acceptButton.onClick.AddListener(() =>
        {
            SoundManager.GetInstance().StopAllLoopingSoundEffects();
            SoundManager.GetInstance().PlaySingle("SoundFX/pop_drip");
            ViewController.GetInstance().CreateView("Prefabs/SeniorCall/SeniorCall");
        });

        declineButton = this.transform.Find("Panel/BottomBar/EndCall").GetComponent<Button>();
        declineButton.onClick.AddListener(() =>
        {
            SoundManager.GetInstance().StopAllLoopingSoundEffects();
            SoundManager.GetInstance().PlaySingle("SoundFX/pop_drip");
            StartCoroutine(StopRotatingDotRings());
        });

        if (UserDataController.GetInstance().ActiveUserType == UserDataController.UserType.Senior)
        {
            UserDataController.GetInstance().CalleeImage = "Linda";
            UserDataController.GetInstance().CalleeImage = "Linda";

            UserDataController.GetInstance().UserImage = "Emma";
            UserDataController.GetInstance().UserName = "Emma";
        }
        else
        {
            UserDataController.GetInstance().CalleeImage = "Emma";
            UserDataController.GetInstance().CalleeImage = "Emma";

            UserDataController.GetInstance().UserImage = "Linda";
            UserDataController.GetInstance().UserName = "Linda";
        }
    }

    protected IEnumerator RotateDotRings()
    {
        RectTransform DotRing = transform.Find("MiddleDial/DotRing").GetComponent<RectTransform>();

        float timeToReachTarget = 2f;

        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToReachTarget;

            DotRing.transform.localScale = Vector3.Lerp(DotRing.transform.localScale, new Vector3(1.25f, 1.25f), t);
            yield return null;
        }

        //float speed = 500;
        while (true)
        {
            DotRing.Rotate(Vector3.forward * Time.deltaTime * 4);
            yield return null;
        }
    }

    protected IEnumerator StopRotatingDotRings()
    {
        if (ieRotateDotRings != null)
        {
            StopCoroutine(ieRotateDotRings);
        }

        RectTransform DotRing = transform.Find("MiddleDial/DotRing").GetComponent<RectTransform>();

        float timeToReachTarget = 2f;

        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToReachTarget;

            DotRing.transform.localScale = Vector3.Lerp(DotRing.transform.localScale, new Vector3(1f, 1f), t);
            yield return null;
        }

        ViewController.GetInstance().CreateView("Prefabs/ContactsScreen/ContactsScreen");
    }
}
