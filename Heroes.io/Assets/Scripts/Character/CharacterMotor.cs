using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(CharacterStats), typeof(Rigidbody))]
public class CharacterMotor : MonoBehaviour {

    private Vector2 _remoteInput;
    private Quaternion _remoteRotation;
    private float _remoteRotationSpeed = 12f;

    private CharacterStats _characterStats;
    private Rigidbody _rb;
    private PhotonView _photonView;

    public bool IsMoving { get { return _rb.velocity.magnitude > 0f ? true : false; } }
    public float VelocityMagnitude { get { return _rb.velocity.magnitude; } }

    private void Awake() {
        _characterStats = GetComponent<CharacterStats>();
        _rb = GetComponent<Rigidbody>();
        _photonView = GetComponent<PhotonView>();
    }

    private void FixedUpdate() {
        if (!_photonView.IsMine) {
            ProcessRemoteInput();
            ProcessRemoteRotation();
        }
    }

    private void ProcessRemoteInput() {
        _rb.velocity = new Vector3(_remoteInput.x * _characterStats.GetMovementSpeed(), 0f, _remoteInput.y * _characterStats.GetMovementSpeed());
    }

    private void ProcessRemoteRotation() {
        if (_remoteRotation.eulerAngles.magnitude <= 0) {
            return;
        }

        _rb.MoveRotation(Quaternion.Lerp(transform.rotation, _remoteRotation, Time.fixedDeltaTime * _remoteRotationSpeed));
    }

    public Vector3 GetCurrentPosition() {
        return new Vector3(_rb.position.x, 0f, _rb.position.z);
    }

    public Quaternion GetCurrentRotation() {
        return transform.rotation;
    }

    public void SetRemoteInput(Vector2 input) {
        _remoteInput = input;
    }

    public void SetRemoteRotation(Quaternion rotation) {
        _remoteRotation = rotation;
    }

    public void MoveToLocalInput(Vector2 input) {
        _rb.velocity = new Vector3(input.x, 0f, input.y) * _characterStats.GetMovementSpeed();
    }

    public void RotateToLocalInput(Vector2 input) {
        if (input.magnitude <= 0) {
            return;
        }

        _rb.MoveRotation(transform.rotation = Quaternion.LookRotation(new Vector3(input.x, 0f, input.y)));
    }

}
