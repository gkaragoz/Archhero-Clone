using UnityEngine;

[RequireComponent(typeof(CharacterStats), typeof(Rigidbody))]
public class CharacterMotor : MonoBehaviour {

    private CharacterStats _characterStats;
    private CharacterAttack _characterAttack;
    private CharacterTargetSelector _characterTargetSelector;
    private Rigidbody _rb;

    public bool IsMoving { get { return _rb.velocity.magnitude > 0f ? true : false; } }
    public float VelocityMagnitude { get { return _rb.velocity.magnitude; } }

    private void Awake() {
        _characterStats = GetComponent<CharacterStats>();
        _characterAttack = GetComponent<CharacterAttack>();
        _characterTargetSelector = GetComponent<CharacterTargetSelector>();
        _rb = GetComponent<Rigidbody>();

        _characterAttack.onAttackStarted += LookToTarget;
    }

    private void LookToTarget() {
        if (_characterTargetSelector.HasTarget) {
            Vector3 desiredLookPosition = _characterTargetSelector.SelectedTarget.transform.position - transform.position;
            desiredLookPosition.y = 0;

            if (desiredLookPosition != Vector3.zero) {
                Quaternion rotation = Quaternion.LookRotation(desiredLookPosition);

                _rb.MoveRotation(rotation);
            }
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

    public void MoveToInput(Vector2 input) {
        _rb.velocity = new Vector3(input.x, 0f, input.y) * _characterStats.GetMovementSpeed();
    }

    public void RotateToInput(Vector2 input) {
        if (input.magnitude <= 0) {
            return;
        }

        _rb.MoveRotation(transform.rotation = Quaternion.LookRotation(new Vector3(input.x, 0f, input.y)));
    }

}
