using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncomingCall : MonoBehaviour
{
    Button acceptButton;
    Button declineButton;
    // Use this for initialization
    void Start ()
    {
        SoundManager.GetInstance().PlaySingle("SoundFX/Ringing_Phone", true);

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
            ViewController.GetInstance().CreateView("Prefabs/ContactsScreen/ContactsScreen");
        });

        StartCoroutine(RotateDotRings());
    }

    protected IEnumerator RotateDotRings()
    {

        RectTransform DotRing = transform.Find("MiddleDial/DotRing").GetComponent<RectTransform>();

        //float speed = 500;
        while (true)
        {
            DotRing.Rotate(Vector3.forward * Time.deltaTime * 4);
            yield return null;
        }
    }
}
