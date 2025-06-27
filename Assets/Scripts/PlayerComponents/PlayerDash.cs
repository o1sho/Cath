using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    private float _dashCooldownTimer;

    public float DashDuration => dashDuration;
    public float DashCooldown => dashCooldown;
    public float DashCooldownTimer => _dashCooldownTimer;

    public bool CanDash => _dashCooldownTimer <= 0;

    public void StartDash(Vector2 direction, PlayerMovement movement) {
        _dashCooldownTimer = dashCooldown;
        movement.SetVelocity(direction * dashSpeed);
    }

    public void UpdateCooldown(float deltaTime) {
        if (_dashCooldownTimer > 0) {
            _dashCooldownTimer -= deltaTime;
        }
    }

    public void ResetCooldown() {
        _dashCooldownTimer = 0f;
    }
}
