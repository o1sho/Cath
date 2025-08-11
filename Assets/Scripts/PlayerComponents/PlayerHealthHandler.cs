using System;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerHealthHandler : MonoBehaviour, IPlayerComponent, IDamageable {
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private float invulnDuration = 1f; // i-фреймы после удара
    //[SerializeField] private bool invulnWhileDashing = true;

    //public event Action<float> OnInvulnChanged; // 1 Ч включено, 0 Ч выключено (дл€ блинка)

    public int CurrentHealth { get; private set; }
    public bool IsInvulnerable { get; private set; }
    public DamageInfo LastDamage { get; private set; }

    private Player _player;
    private float _invulnTimer;

    //---------------
    public void Init(Player player) {
        _player = player;
        CurrentHealth = maxHealth;
        _invulnTimer = 0f;
        IsInvulnerable = false;
    }
    //---------------

    private void Update() {
        if (_invulnTimer > 0f) {
            _invulnTimer -= Time.deltaTime;
            if (_invulnTimer <= 0f) {
                _invulnTimer = 0f;
                SetInvuln(false);
            }
        }
    }

    public bool TakeDamage(DamageInfo info) {
        if (IsInvulnerable) return false;

        LastDamage = info;
        CurrentHealth = Math.Max(0, CurrentHealth - info.Amount);

        _invulnTimer = invulnDuration;
        SetInvuln(true);

        if (CurrentHealth <= 0)
            _player.ChangeState(_player.HurtState);

        return true;
    }

    public void Heal(int amount) {
        CurrentHealth = Math.Clamp(CurrentHealth + amount, 0, maxHealth);
    }

    private void SetInvuln(bool v) {
        IsInvulnerable = v;
        //OnInvulnChanged?.Invoke(v ? 1f : 0f);
    }
}
