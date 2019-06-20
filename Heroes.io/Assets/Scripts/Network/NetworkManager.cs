using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks {

    public string username;

    private void Start() {
        username = "Player " + Random.Range(1000, 10000);

        PhotonNetwork.LocalPlayer.NickName = username;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.SendRate = 10;
        PhotonNetwork.SerializationRate = 10;

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        Debug.Log("OnConnectedToMaster");

        Debug.Log("Trying to JoinRandomRoom");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.Log("OnCreateRoomFailed: " + message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message) {
        Debug.Log("OnJoinRandomFailed: " + message);
        Debug.Log("Creating a new room.");

        string roomName = "Room " + Random.Range(1000, 10000);

        RoomOptions options = new RoomOptions { MaxPlayers = 5 };

        Debug.Log("roomName: " + roomName + "(" + options.MaxPlayers + ")");
        PhotonNetwork.CreateRoom(roomName, options, null);
    }

}
