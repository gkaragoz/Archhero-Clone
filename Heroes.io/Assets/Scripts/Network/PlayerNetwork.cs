using Photon.Pun;
using UnityEngine;

public class PlayerNetwork : MonoBehaviourPun, IPunObservable {

    private PlayerController _playerController;

    private void Awake() {
        _playerController = GetComponent<PlayerController>();

        //destroy the controller if the player is not controlled by me
        if (!photonView.IsMine && _playerController != null) {
            _playerController.IsRemotePlayer = true;
            _playerController.DestroyJoystick();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(_playerController.GetCurrentPosition());
            stream.SendNext(_playerController.GetCurrentRotation());
            stream.SendNext(_playerController.GetCurrentVelocity());
        } else {
            _playerController.SetRemotePosition((Vector3)stream.ReceiveNext());
            _playerController.SetRemoteRotation((Quaternion)stream.ReceiveNext());
            _playerController.SetRemoteVelocity((Vector3)stream.ReceiveNext());
        }
    }

}
