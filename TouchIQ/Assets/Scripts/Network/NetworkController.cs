using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : Photon.MonoBehaviour
{

    private bool connectedToMaster = false;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(Connect());
        
    }
	
    IEnumerator Connect()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
            PhotonNetwork.ConnectUsingSettings("1.0");

        while (!connectedToMaster)
        {
            yield return null;
        }

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.CleanupCacheOnLeave = true;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        TypedLobby typedLobby = new TypedLobby();
        typedLobby.Name = "chat";
        typedLobby.Type = LobbyType.Default;
        PhotonNetwork.JoinLobby(typedLobby);

        if (PhotonNetwork.GetRoomList().Length > 0)
        {
            UnityEngine.Debug.Log("ROOM EXISTS");
        }

        //PhotonNetwork.JoinOrCreateRoom("MidnightVideoChat", roomOptions, typedLobby);
        //PhotonNetwork.CreateRoom("MidnightVideoChat", roomOptions, typedLobby);
        PhotonNetwork.NetworkStatisticsEnabled = true;
        //PhotonNetwork.GetRoomList()
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        if (connectedToMaster)
        {
            GUILayout.Label("Player Count: " + PhotonNetwork.countOfPlayers.ToString());
            GUILayout.Label("Room Count: " + PhotonNetwork.GetRoomList().Length.ToString());
        }
        
    }

    void OnJoinedLobby()
    {
        UnityEngine.Debug.Log("OnJoinedLobby");
    }
    void OnReceivedRoomListUpdate()
    {
        UnityEngine.Debug.Log("OnReceivedRoomListUpdate: " + PhotonNetwork.GetRoomList().Length);
        if(PhotonNetwork.GetRoomList().Length > 0)
        {
            if (ViewController.GetInstance().CurrentView.name.Contains("Contact"))
            {
                ViewController.GetInstance().CreateView("Prefabs/SeniorCall/SeniorCall");
            }
        }
    }

    void OnJoinedRoom()
    {
        UnityEngine.Debug.Log("OnJoinedRoom");
    }

    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        UnityEngine.Debug.Log(newPlayer.ToStringFull());
    }

    void OnConnectedToMaster()
    {
        UnityEngine.Debug.Log("OnConnectedToMaster");
        connectedToMaster = true;
    }
}
