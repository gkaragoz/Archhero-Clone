using System;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterAttack : MonoBehaviour {

    public Action onAttack;

    private CharacterStats _characterStats;

    private void Awake() {
        _characterStats = GetComponent<CharacterStats>();
    }

    public void Attack() {
        onAttack?.Invoke();
    }

}
