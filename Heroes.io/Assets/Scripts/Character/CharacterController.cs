using System;
using UnityEngine;

[RequireComponent(typeof(CharacterMotor), typeof(CharacterAttack), typeof(CharacterStats))]
public class CharacterController : MonoBehaviour {

    public Action<CharacterController> onDead;
    public Action onTakeDamage;

    [SerializeField]
    private GameObject _getDamageFX = null;

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

    public void SetRemoteInput(Vector2 input) {
        _characterMotor.SetRemoteInput(input);
    }

    public void SetRemoteRotation(Quaternion rotation) {
        _characterMotor.SetRemoteRotation(rotation);
    }

    public void MoveToLocalInput(Vector2 input) {
        _characterMotor.MoveToLocalInput(input);
    }

    public void RotateToLocalInput(Vector2 input) {
        _characterMotor.RotateToLocalInput(input);
    }

    public void FireGetDamageFX(Vector3 hitPoint) {
        Instantiate(_getDamageFX, hitPoint, Quaternion.identity);
    }

    public void Attack() {
        _characterAttack.Attack();
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

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Projectile") {
            Vector3 hitPoint = new Vector3(other.transform.position.x, 0f, other.transform.position.z);
            FireGetDamageFX(hitPoint);

            Destroy(other.gameObject);
        }
    }

}
