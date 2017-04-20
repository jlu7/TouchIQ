using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataController : MonoBehaviour {

    private static UserDataController userDataController;

    public static UserDataController GetInstance()
    {
        if (userDataController == null)
        {
            userDataController = new GameObject("UserDataController").AddComponent<UserDataController>();
        }
        return userDataController;
    }

    public ContactsUsersModel ContactsUsers;

    public void Initialize()
    {
        TextAsset ta = Resources.Load<TextAsset>("Configs/ContactsUsers");
        ContactsUsers = Newtonsoft.Json.JsonConvert.DeserializeObject<ContactsUsersModel>(ta.text);
        UnityEngine.Debug.Log(ContactsUsers.Caregiver);
        //var json = JsonUtility.FromJson<Dictionary<string,object>>(ta.text);
        //UnityEngine.Debug.Log(json["Caregiver"]);
    }

}
