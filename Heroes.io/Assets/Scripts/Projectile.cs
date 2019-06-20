using UnityEngine;

public class Projectile : MonoBehaviour, IPooledObject {

    [Header("Initialization")]
    [SerializeField]
    private float _speed = 20f;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private float _damage = 0f;

    private Rigidbody _rb;

    public float Damage {
        get { return _damage; }
        set { _damage = value; }
    }

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    public void Fire(Vector3 direction) {
        _rb.velocity = direction * _speed;

        if (_rb.velocity.magnitude > 0) {
            _rb.rotation = Quaternion.LookRotation(_rb.velocity);
        }
    }

    public void OnObjectReused() {
        _rb.velocity = Vector3.zero;
        gameObject.SetActive(true);
    }

}
