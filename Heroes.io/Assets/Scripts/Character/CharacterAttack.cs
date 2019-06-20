using System;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterAttack : MonoBehaviour {

    public Action onAttack;

    [Header("Initialization")]
    [SerializeField]
    private Transform _projectileSpawnTransform = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private Transform _targetTransform = null;
    [SerializeField]
    [Utils.ReadOnly]
    private float _nextAttack = 0;
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isAttacking = false;

    private CharacterStats _characterStats;
    private CharacterController _characterController;
    private CharacterMotor _characterMotor;
    private CharacterTargetSelector _characterTargetSelector;

    public bool IsAttacking { get { return _isAttacking; } }

    private void Awake() {
        _characterController = GetComponent<CharacterController>();
        _characterMotor = GetComponent<CharacterMotor>();
        _characterStats = GetComponent<CharacterStats>();
        _characterTargetSelector = GetComponent<CharacterTargetSelector>();
    }

    private void Update() {
        if (_characterController.IsMoving && _isAttacking) {
            StopAttacking();
            return;
        }

        if (_isAttacking) {
            Attack();
        }
    }

    private void Attack() {
        _isAttacking = true;

        _characterTargetSelector.SearchTarget();

        if (Time.time <= _nextAttack) {
            return;
        }

        _nextAttack = Time.time + _characterStats.GetAttackRate();

        onAttack?.Invoke();

        // Initialize projectile physics.
        Projectile projectile = ObjectPooler.instance.SpawnFromPool("Arrow", _projectileSpawnTransform.position, Quaternion.identity).GetComponent<Projectile>();
        Vector3 direction = (_characterTargetSelector.SelectedTarget.transform.position - _projectileSpawnTransform.position).normalized;

        // Set projectile damage.
        projectile.Damage = _characterStats.GetAttackDamage();

        // Force for apply to projectile.
        projectile.Fire(direction);
    }

    public void StartAttacking() {
        _isAttacking = true;
    }

    public void StopAttacking() {
        _isAttacking = false;
    }

}
