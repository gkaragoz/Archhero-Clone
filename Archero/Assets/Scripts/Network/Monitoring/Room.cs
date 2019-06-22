using Photon.Pun;
using TMPro;
using UnityEngine;

public class Room : MonoBehaviour {

    [SerializeField]
    private string _roomInfoMessage = "Tickrate: ";

    [SerializeField]
    private TextMeshProUGUI _txtRoomInfo = null;

    private void Update() {
        if (PhotonNetwork.IsConnectedAndReady) {
            _txtRoomInfo.text = _roomInfoMessage + PhotonNetwork.SendRate + ":" + PhotonNetwork.SerializationRate;
        } else {
            _txtRoomInfo.text = "n/a";
        }
    }

}
