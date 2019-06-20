using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(CharacterStats), typeof(Rigidbody))]
public class CharacterMotor : MonoBehaviour {

    private Vector3 _remotePosition;
    private Quaternion _remoteRotation;
    private Vector3 _remoteVelocity;
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
            _rb.MovePosition(Vector3.Lerp(transform.position, _remotePosition, Time.fixedDeltaTime));
            _rb.MoveRotation(Quaternion.Lerp(transform.rotation, _remoteRotation, Time.fixedDeltaTime * _remoteRotationSpeed));
            _rb.velocity = _remoteVelocity;
        }
    }

    public Vector3 GetCurrentPosition() {
        return new Vector3(_rb.position.x, 0f, _rb.position.z);
    }

    public Quaternion GetCurrentRotation() {
        return transform.rotation;
    }

    public Vector3 GetCurrentVelocity() {
        return _rb.velocity;
    }

    public void SetRemotePosition(Vector3 position) {
        _remotePosition = position;
    }

    public void SetRemoteRotation(Quaternion rotation) {
        _remoteRotation = rotation;
    }

    public void SetRemoteVelocity(Vector3 velocity) {
        _remoteVelocity = velocity;
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
