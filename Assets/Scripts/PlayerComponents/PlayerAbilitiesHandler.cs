using System;
using System.Collections.Generic;
using UnityEngine;

public enum Ability {
    Dash
}

public class PlayerAbilitiesHandler : MonoBehaviour, IPlayerComponent {

    private Player _player;
    private readonly HashSet<Ability> _unlocked = new HashSet<Ability>();

    public event Action<Ability, bool> OnAbilityChanged;

    [Header("Start Abilities")]
    [SerializeField] private bool dashUnlockedAtStart = false;

    public void Init(Player player) {
        _player = player;
    }

    public bool Can(Ability ability) => _unlocked.Contains(ability);

    public void Unlock(Ability ability) {
        if (_unlocked.Add(ability)) {
            OnAbilityChanged?.Invoke(ability, true);
        }
    }

    public void Lock(Ability ability) {
        if (_unlocked.Remove(ability)) {
            OnAbilityChanged?.Invoke(ability, false);
        }
    }

    private void Start() {
        if (dashUnlockedAtStart) Unlock(Ability.Dash);
    }
}
