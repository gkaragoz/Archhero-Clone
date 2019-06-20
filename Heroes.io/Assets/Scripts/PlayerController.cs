using UnityEngine;

public class PlayerController : MonoBehaviour {

    public bool IsRemotePlayer { get; set; }

    public Vector2 CurrentInput { get; set; }

    public bool HasInput { get { return (CurrentInput != Vector2.zero) ? true : false; } }

    public CharacterController CharacterController { get { return _characterController; } }

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private float _xInput;
    [SerializeField]
    [Utils.ReadOnly]
    private float _yInput;
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterController _characterController;
    [SerializeField]
    [Utils.ReadOnly]
    private Joystick _joystick = null;

    private void Awake() {
        _characterController = GetComponentInChildren<CharacterController>();
    }

    private void Update() {
        if (IsRemotePlayer) {
            return;
        }

        if (_joystick == null) {
            Debug.Log("Joystick is missing!");
            return;
        }

        _xInput = _joystick.Horizontal;
        _yInput = _joystick.Vertical;

        CurrentInput = new Vector2(_xInput, _yInput);

        if (HasInput) {
            MoveToCurrentInput();
            RotateToCurrentInput();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            Attack();
        }
    }

    public void DestroyJoystick() {
        Destroy(_joystick.gameObject);
    }

    public Vector3 GetCurrentPosition() {
        return _characterController.GetCurrentPosition();
    }

    public Quaternion GetCurrentRotation() {
        return _characterController.GetCurrentRotation();
    }

    public Vector3 GetCurrentVelocity() {
        return _characterController.GetCurrentVelocity();
    }

    public void SetRemotePosition(Vector3 position) {
        _characterController.SetRemotePosition(position);
    }

    public void SetRemoteRotation(Quaternion rotation) {
        _characterController.SetRemoteRotation(rotation);
    }

    public void SetRemoteVelocity(Vector3 velocity) {
        _characterController.SetRemoteVelocity(velocity);
    }

    public void MoveToCurrentInput() {
        _characterController.MoveToLocalInput(CurrentInput);
    }

    public void RotateToCurrentInput() {
        _characterController.RotateToLocalInput(CurrentInput);
    }

    public void Attack() {
        _characterController.Attack();
    }

    public void Destroy() {
        Destroy(this.gameObject);
    }

}
