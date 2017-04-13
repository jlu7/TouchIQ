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

        acceptButton = this.transform.Find("Panel/AcceptButton").GetComponent<Button>();
        acceptButton.onClick.AddListener(() =>
        {
            SoundManager.GetInstance().StopAllLoopingSoundEffects();
            ViewController.GetInstance().CreateView("Prefabs/SeniorCall/SeniorCall");
        });

        declineButton = this.transform.Find("Panel/DeclineButton").GetComponent<Button>();
        declineButton.onClick.AddListener(() =>
        {
            SoundManager.GetInstance().StopAllLoopingSoundEffects();
            ViewController.GetInstance().CreateView("Prefabs/ContactsScreen/ContactsScreen");
        });
    }
}
