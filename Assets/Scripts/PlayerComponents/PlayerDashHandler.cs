using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerDashHandler : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    private float _dashCooldownTimer;
    private Player _player;

    public float DashDuration => dashDuration;
    public float DashCooldown => dashCooldown;
    public float DashCooldownTimer => _dashCooldownTimer;

    public bool CanDash => _dashCooldownTimer <= 0;

    //---------------
    public void Init(Player player) {
        _player = player;
    }
    //---------------

    public void TriggerCooldown() {
        _dashCooldownTimer = dashCooldown;
    }

    public float GetDashSpeed() => dashSpeed;

    public void UpdateCooldown(float deltaTime) {
        if (_dashCooldownTimer > 0) {
            _dashCooldownTimer -= deltaTime;
        }
    }

    public void ResetCooldown() {
        _dashCooldownTimer = 0f;
    }
}
