using UnityEngine;

[CreateAssetMenu(fileName = "Character Stats", menuName = "Scriptable Objects/Character Stats")]
public class CharacterStats_SO : ScriptableObject {

    #region Properties

    [SerializeField]
    private string _name = "Character";

    [SerializeField]
    private GameObject _prefab;

    // Health
    [SerializeField]
    private float _currentHealth;
    [SerializeField]
    private float _maxHealth;

    // Attack
    [SerializeField]
    [Range(1, 89)]
    private float _shootAngle = 45f;
    [SerializeField]
    private float _attackRate = 1f;
    [SerializeField]
    private float _maxAttackRate = 0.25f;
    [SerializeField]
    private float _attackDamage = 50f;
    [SerializeField]
    private float _minAttackDamage = 1f;

    // Movement
    [SerializeField]
    private float _movementSpeed = 5f;

    // Levelling
    [SerializeField]
    private int _level = 0;
    [SerializeField]
    private int _maxExperience = 0;
    [SerializeField]
    private int _currentExperience = 0;

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public GameObject Prefab {
        get { return _prefab; }
        set { _prefab = value; }
    }

    public float CurrentHealth {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    public float MaxHealth {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public float ShootAngle {
        get { return _shootAngle; }
        set { _shootAngle = value; }
    }

    public float AttackRate {
        get { return _attackRate; }
        set { _attackRate = value; }
    }

    public float MaxAttackRate {
        get { return _maxAttackRate; }
    }

    public float AttackDamage {
        get { return _attackDamage; }
        set { _attackDamage = value; }
    }

    public float MinAttackDamage {
        get { return _minAttackDamage; }
    }

    public float MovementSpeed {
        get { return _movementSpeed; }
        set { _movementSpeed = value; }
    }

    public int Level {
        get { return _level; }
        set { _level = value; }
    }

    public int MaxExperience {
        get { return _maxExperience; }
        set { _maxExperience = value; }
    }

    public int CurrentExperience {
        get { return _currentExperience; }
        set { _currentExperience = value; }
    }

    #endregion

}
