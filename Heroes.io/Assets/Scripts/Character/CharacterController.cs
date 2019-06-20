using UnityEngine;

[RequireComponent(typeof(CharacterMotor), typeof(CharacterAttack), typeof(CharacterStats))]
public class CharacterController : MonoBehaviour {

    [SerializeField]
    private GameObject _getDamageFX = null;

    private CharacterMotor _characterMotor;
    private CharacterAttack _characterAttack;
    private CharacterStats _characterStats;

    public bool IsMoving { get { return _characterMotor.IsMoving; } }

    private void Awake() {
        _characterMotor = GetComponent<CharacterMotor>();
        _characterAttack = GetComponent<CharacterAttack>();
        _characterStats = GetComponent<CharacterStats>();
    }

    public Vector3 GetCurrentPosition() {
        return _characterMotor.GetCurrentPosition();
    }

    public Quaternion GetCurrentRotation() {
        return _characterMotor.GetCurrentRotation();
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

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Projectile") {
            Vector3 hitPoint = new Vector3(other.transform.position.x, 0f, other.transform.position.z);
            FireGetDamageFX(hitPoint);

            Destroy(other.gameObject);
        }
    }

}
