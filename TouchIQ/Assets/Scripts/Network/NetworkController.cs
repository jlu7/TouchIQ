using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : Photon.MonoBehaviour
{
    private static NetworkController networkController;

    public static NetworkController GetInstance()
    {
        if (networkController == null)
        {
            networkController = new GameObject("NetworkController").AddComponent<NetworkController>();
        }
        return networkController;
    }
    public delegate void PhotoReceived(string setName, string photoName);
    public event PhotoReceived OnPhotoReceived = (string setName, string photoName) => { };

    private bool connectedToMaster = false;

    private PhotonView NetworkView;

	
    public IEnumerator Connect()
    {
        NetworkView = gameObject.AddComponent<PhotonView>();
        NetworkView.synchronization = ViewSynchronization.Off;
        NetworkView.viewID = 3;

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
        if (CheatController.GetInstance().DebugEnabled)
        {
            GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
            if (connectedToMaster)
            {
                GUILayout.Label("Player Count: " + PhotonNetwork.countOfPlayers.ToString());
                GUILayout.Label("Room Count: " + PhotonNetwork.GetRoomList().Length.ToString());
            }
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
                ViewController.GetInstance().CurrentView.GetComponent<ContactsList>().IncomingCall();
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

    public void SendPhotoMessage(string setName, string photoName)
    {
        UnityEngine.Debug.LogWarning("Sent Photo: " + setName + " , " + photoName);
        NetworkView.RPC("PhotoMessageReceived", PhotonTargets.Others, setName, photoName);
    }

    [PunRPC]
    public void PhotoMessageReceived(string setName, string photoName)
    {
        UnityEngine.Debug.LogWarning("PhotoMessageReceived: " + photoName);
        OnPhotoReceived(setName, photoName);
    }
}
