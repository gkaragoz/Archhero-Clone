using Photon.Pun;
using TMPro;
using UnityEngine;

public class Ping : MonoBehaviour {

    [SerializeField]
    private string _pingMessage = "Ping: ";

    [SerializeField]
    private TextMeshProUGUI _txtPing = null;

    private int _cache = -1;

    private void Update() {
        if (PhotonNetwork.IsConnectedAndReady) {
            if (PhotonNetwork.GetPing() != _cache) {
                _cache = PhotonNetwork.GetPing();
                _txtPing.text = _pingMessage + _cache.ToString() + " ms";
            }
        } else {
            if (_cache != -1) {
                _cache = -1;
                _txtPing.text = "n/a";
            }
        }
    }

}
