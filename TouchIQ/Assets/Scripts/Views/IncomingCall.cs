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

        acceptButton = this.transform.Find("Panel/BottomBar2/AcceptCall").GetComponent<Button>();
        acceptButton.onClick.AddListener(() =>
        {
            SoundManager.GetInstance().StopAllLoopingSoundEffects();
            SoundManager.GetInstance().PlaySingle("SoundFX/pop_drip");
            ViewController.GetInstance().CreateView("Prefabs/SeniorCall/SeniorCall");
        });

        declineButton = this.transform.Find("Panel/BottomBar2/EndCall").GetComponent<Button>();
        declineButton.onClick.AddListener(() =>
        {
            SoundManager.GetInstance().StopAllLoopingSoundEffects();
            SoundManager.GetInstance().PlaySingle("SoundFX/pop_drip");
            ViewController.GetInstance().CreateView("Prefabs/ContactsScreen/ContactsScreen");
        });
    }
}
