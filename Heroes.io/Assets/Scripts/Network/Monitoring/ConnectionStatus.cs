using UnityEngine;
using Photon.Pun;
using TMPro;

public class ConnectionStatus : MonoBehaviour {

    [SerializeField]
    private string _connectionStatusMessage = "Connection Status: ";

    [SerializeField]
    private TextMeshProUGUI _txtConnectionStatus;

    private void Update() {
        _txtConnectionStatus.text = _connectionStatusMessage + PhotonNetwork.NetworkClientState;
    }

}
