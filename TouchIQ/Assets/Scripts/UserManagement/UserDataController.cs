using System;
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

    public enum UserType
    {
        Caregiver,
        Senior
    }
    public UserType ActiveUserType = UserType.Caregiver;

    public void Initialize()
    {
        string userTypeString = PlayerPrefs.GetString("UserType", UserType.Caregiver.ToString());
        UserType userTypeFromPrefs = UserType.Caregiver;
        try
        {
            userTypeFromPrefs = (UserType)Enum.Parse(typeof(UserType), userTypeString, true);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError(e);
        }
        
        ActiveUserType = userTypeFromPrefs;
        TextAsset ta = Resources.Load<TextAsset>("Configs/ContactsUsers");
        ContactsUsers = Newtonsoft.Json.JsonConvert.DeserializeObject<ContactsUsersModel>(ta.text);
        UnityEngine.Debug.Log(ContactsUsers.Caregiver);
        //var json = JsonUtility.FromJson<Dictionary<string,object>>(ta.text);
        //UnityEngine.Debug.Log(json["Caregiver"]);
    }

    public void SetUserType(UserType userType)
    {
        if(userType != ActiveUserType)
        {
            PlayerPrefs.SetString("UserType", userType.ToString());
        }
    }

}
