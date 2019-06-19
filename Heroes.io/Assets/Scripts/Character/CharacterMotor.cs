using UnityEngine;

[RequireComponent(typeof(CharacterStats), typeof(Rigidbody))]
public class CharacterMotor : MonoBehaviour {

    private CharacterStats _characterStats;
    private Rigidbody _rb;

    private void Awake() {
        _characterStats = GetComponent<CharacterStats>();
        _rb = GetComponentInChildren<Rigidbody>();
    }

    public Vector3 GetCurrentPosition() {
        return new Vector3(_rb.position.x, 0f, _rb.position.z);
    }

    public Quaternion GetCurrentRotation() {
        return transform.rotation;
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
