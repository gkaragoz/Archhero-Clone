using System;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterAttack : MonoBehaviour {

    public Action onAttackStarted;
    public Action onAttackStopped;

    [Header("Initialization")]
    [SerializeField]
    private Transform _projectileSpawnTransform = null;
    private CharacterStats _characterStats;
    private CharacterController _characterController;
    private CharacterMotor _characterMotor;
    private CharacterTargetSelector _characterTargetSelector;

    public bool IsAttacking { get; private set; }

    private void Awake() {
        _characterController = GetComponent<CharacterController>();
        _characterMotor = GetComponent<CharacterMotor>();
        _characterStats = GetComponent<CharacterStats>();
        _characterTargetSelector = GetComponent<CharacterTargetSelector>();
    }

    private void Update() {
        if (IsAttacking) {
            _characterTargetSelector.SearchTarget();
        }
    }

    /// <summary>
    /// This function triggered from AnimationTrigger that attachted animator component.
    /// <see cref="AnimationTrigger"/>
    /// </summary>
    public void AttackEvent() {
        if (!IsAttacking) {
            Debug.Log("Attacking is interrupted.");
            return;
        }

        Debug.Log("Attacked");

        // Initialize projectile physics.
        Projectile projectile = ObjectPooler.instance.SpawnFromPool("Arrow", _projectileSpawnTransform.position, Quaternion.identity).GetComponent<Projectile>();
        Vector3 direction = (_characterTargetSelector.SelectedTarget.transform.position - _projectileSpawnTransform.position).normalized;

        // Set projectile damage.
        projectile.Damage = _characterStats.GetAttackDamage();

        // Force for apply to projectile.
        projectile.Fire(direction);
    }

    public void StartAttacking() {
        IsAttacking = true;

        onAttackStarted?.Invoke();
    }

    public void StopAttacking() {
        IsAttacking = false;

        onAttackStopped?.Invoke();
    }

}
