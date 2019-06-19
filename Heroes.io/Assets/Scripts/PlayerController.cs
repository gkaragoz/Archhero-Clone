using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private Joystick _joystick = null;

    public Vector2 CurrentInput { get; set; }

    public bool HasInput {
        get {
            return (CurrentInput != Vector2.zero) ? true : false;
        }
    }

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private float _xInput, _yInput;
    private CharacterController _characterController;

    private void Awake() {
        _characterController = GetComponentInChildren<CharacterController>();
    }

    private void Update() {
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
    }

    public Vector3 GetCurrentPosition() {
        return _characterController.GetCurrentPosition();
    }

    public Quaternion GetCurrentRotation() {
        return _characterController.GetCurrentRotation();
    }

    public void MoveToCurrentInput() {
        _characterController.MoveToLocalInput(CurrentInput);
    }

    public void RotateToCurrentInput() {
        _characterController.RotateToLocalInput(CurrentInput);
    }

    public void Destroy() {
        Destroy(this.gameObject);
    }

}
