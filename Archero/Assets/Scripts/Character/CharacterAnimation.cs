using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(CharacterMotor), typeof(CharacterAttack))]
public class CharacterAnimation : MonoBehaviour {

    [SerializeField]
    private Animator _animator = null;

    private CharacterController _characterController;
    private CharacterMotor _characterMotor;
    private CharacterAttack _characterAttack;

    private const string VELOCITY_STATE = "Velocity";
    private const string DIE = "Die";
    private const string DEAD_SELECTOR = "INT_DeadSelector";
    private const string IS_ATTACKING = "IsAttacking";

    private void Awake() {
        _characterController = GetComponentInChildren<CharacterController>();
        _characterMotor = GetComponentInChildren<CharacterMotor>();
        _characterAttack = GetComponentInChildren<CharacterAttack>();

        _characterAttack.onAttackStarted += OnAttackStarted;
        _characterAttack.onAttackStopped += OnAttackStopped;
    }

    private void Update() {
        OnMovement();
    }

    public void OnMovement() {
        if (_characterController.IsMoving) {
            _animator.SetFloat(VELOCITY_STATE, _characterMotor.VelocityMagnitude, 0.1f, Time.deltaTime);
        } else {
            _animator.SetFloat(VELOCITY_STATE, 0);
        }
    }

    public void OnAttackStarted() {
        Debug.Log("Set IsAttacking true");
        _animator.SetBool(IS_ATTACKING, true);
    }

    public void OnAttackStopped() {
        Debug.Log("Set IsAttacking false");
        _animator.SetBool(IS_ATTACKING, false);
    }

    public void OnDead(CharacterController characterController) {
        int random = Random.Range(0, 2);
        _animator.SetInteger(DEAD_SELECTOR, random);
        _animator.SetTrigger(DIE);
    }

}
