using System;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterAttack : MonoBehaviour {

    public Action onAttackStarted;
    public Action onAttackStopped;

    [Header("Initialization")]
    [SerializeField]
    private Transform _projectileSpawnTransform = null;

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

    /// <summary>
    /// This function triggered from AnimationTrigger that attachted animator component.
    /// <see cref="AnimationTrigger"/>
    /// </summary>
    public void AttackEvent() {
        if (!_isAttacking) {
            Debug.Log("Attacking is interrupted.");
            return;
        }

        Debug.Log("Attacked");

        _characterTargetSelector.SearchTarget();

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

        onAttackStarted?.Invoke();
    }

    public void StopAttacking() {
        _isAttacking = false;

        onAttackStopped?.Invoke();
    }

}
