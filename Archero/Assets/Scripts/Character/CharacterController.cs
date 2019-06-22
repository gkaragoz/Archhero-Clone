using System;
using UnityEngine;

[RequireComponent(typeof(CharacterMotor), typeof(CharacterAttack), typeof(CharacterStats))]
public class CharacterController : MonoBehaviour {

    public Action<CharacterController> onDead;
    public Action onTakeDamage;

    private CharacterMotor _characterMotor;
    private CharacterAttack _characterAttack;
    private CharacterStats _characterStats;

    public bool IsMoving { get { return _characterMotor.IsMoving; } }
    public float CurrentHealth { get { return _characterStats.GetCurrentHealth(); } }
    public float MaxHealth { get { return _characterStats.GetMaxHealth(); } }

    private void Awake() {
        _characterMotor = GetComponent<CharacterMotor>();
        _characterAttack = GetComponent<CharacterAttack>();
        _characterStats = GetComponent<CharacterStats>();
    }

    private void Update() {
        if (IsMoving && _characterAttack.IsAttacking) {
            Debug.Log("Stop Attacking.");
            StopAttacking();
        } else if (!IsMoving && !_characterAttack.IsAttacking) {
            Debug.Log("Start Attacking.");
            StartAttacking();
        }
    }

    private void Die() {
        onDead?.Invoke(this);

        //_SFXEarnGolds.Play();
    }

    public Vector3 GetCurrentPosition() {
        return _characterMotor.GetCurrentPosition();
    }

    public Quaternion GetCurrentRotation() {
        return _characterMotor.GetCurrentRotation();
    }

    public Vector3 GetCurrentVelocity() {
        return _characterMotor.GetCurrentVelocity();
    }

    public void MoveToInput(Vector2 input) {
        _characterMotor.MoveToInput(input);
    }

    public void RotateToInput(Vector2 input) {
        _characterMotor.RotateToInput(input);
    }

    public void StartAttacking() {
        _characterAttack.StartAttacking();
    }

    public void StopAttacking() {
        _characterAttack.StopAttacking();
    }

    public void TakeDamage(float amount) {
        _characterStats.DecreaseHealth(amount);

        if (_characterStats.GetCurrentHealth() <= 0) {
            Die();
            return;
        }

        //_SFXImpactFlesh.Play();

        onTakeDamage?.Invoke();
    }

}
