using UnityEngine;

public class CharacterStats : MonoBehaviour {

    [Header("Initialization")]
    [SerializeField]
    private CharacterStats_SO _characterDefinition_Template = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterStats_SO _character;

    #region Initializations

    private void Awake() {
        if (_characterDefinition_Template != null) {
            _character = Instantiate(_characterDefinition_Template);
        }
    }

    #endregion

    #region Stat Increasers

    public void ApplyHealth(int healthAmount) {
        if ((_character.CurrentHealth + healthAmount) > _character.MaxHealth) {
            _character.CurrentHealth = _character.MaxHealth;
        } else {
            _character.CurrentHealth += healthAmount;
        }
    }

    public void AddAttackDamage(int damageAmount) {
        _character.AttackDamage += damageAmount;
    }

    public void AddAttackSpeed(float speedAmount) {
        _character.AttackSpeed += speedAmount;
    }

    public void AddAttackRange(float rangeAmount) {
        _character.AttackRange += rangeAmount;
    }

    public void AddExp(int expAmount) {
        if (_character.CurrentExperience + expAmount >= _character.MaxExperience) {
            int needExpAmount = _character.MaxExperience - _character.CurrentExperience;
            int remainingExpAmount = expAmount - needExpAmount;

            LevelUp();

            if (remainingExpAmount > 0) {
                AddExp(remainingExpAmount);
            } else {
                _character.CurrentExperience += needExpAmount;
            }
        } else {
            _character.CurrentExperience += expAmount;
        }
    }

    #endregion

    #region Stat Reducers

    public void TakeDamage(int amount) {
        _character.CurrentHealth -= amount;

        if (_character.CurrentHealth <= 0) {
            _character.CurrentHealth = 0;
        }
    }

    public void ReduceAttackDamage(int damageAmount) {
        _character.AttackDamage -= damageAmount;

        if (_character.AttackDamage <= 0) {
            _character.AttackDamage = 0;
        }
    }

    public void ReduceAttackSpeed(float speedAmount) {
        _character.AttackSpeed -= speedAmount;

        if (_character.AttackSpeed <= 0) {
            _character.AttackSpeed = 0;
        }
    }

    public void ReduceAttackRange(float rangeAmount) {
        _character.AttackRange -= rangeAmount;

        if (_character.AttackRange <= 0) {
            _character.AttackRange = 0;
        }
    }

    public void LooseExp(int expAmount) {
        _character.CurrentExperience -= expAmount;

        if (_character.CurrentExperience <= 0) {
            _character.CurrentExperience = 0;
        }
    }

    #endregion

    #region Reporters

    public string GetName() {
        return _character.Name;
    }

    public bool IsDeath() {
        return _character.CurrentHealth <= 0;
    }

    public int GetMaxHealth() {
        return _character.MaxHealth;
    }

    public int GetCurrentHealth() {
        return _character.CurrentHealth;
    }

    public int GetAttackDamage() {
        return _character.AttackDamage;
    }

    public float GetAttackSpeed() {
        return _character.AttackSpeed;
    }

    public float GetMovementSpeed() {
        return _character.MovementSpeed;
    }

    public float GetRotationSpeed() {
        return _character.RotationSpeed;
    }

    public float GetAttackRange() {
        return _character.AttackRange;
    }

    public int GetLevel() {
        return _character.Level;
    }

    public int GetMaxExperience() {
        return _character.MaxExperience;
    }

    public int GetCurrentExperience() {
        return _character.CurrentExperience;
    }

    #endregion

    private void LevelUp() {
        _character.Level++;

        _character.CurrentExperience = 0;
        _character.MaxExperience = 10 + (_character.Level * 10) + (int)Mathf.Pow(_character.Level + 1, 3);
    }

}
