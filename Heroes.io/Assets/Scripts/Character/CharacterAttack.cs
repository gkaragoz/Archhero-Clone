using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterAttack : MonoBehaviour {

    private CharacterStats _characterStats;

    private void Awake() {
        _characterStats = GetComponent<CharacterStats>();
    }

}
