using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ContactsUsersModel
{
    [JsonProperty]
    public CaregiverModel Caregiver { get; private set; }
    [JsonProperty]
    public SeniorModel Senior { get; private set; }
}

public class CaregiverModel
{
    [JsonProperty]
    public List<ContactModel> Contacts { get; private set; }
}

public class SeniorModel
{
    [JsonProperty]
    public List<ContactModel> Contacts { get; private set; }
}

public class ContactModel
{
    [JsonProperty]
    public string Name { get; private set; }
    [JsonProperty]
    public string Image { get; private set; }
    [JsonProperty]
    public string TimeToCall { get; private set; }
    [JsonProperty]
    public string LastTalked { get; private set; }
    [JsonProperty]
    public List<string> RequestedOfMe { get; private set; }
    [JsonProperty]
    public List<string> RequestedOfOthers { get; private set; }
}
