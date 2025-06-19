using UnityEngine;

public class PlayerDashingState : IState {
    private readonly Player _player;
    private float _dashTimeLeft;

    public PlayerDashingState(Player player) {
        _player = player;
    }

    public void Enter() {
        _dashTimeLeft = _player.DashDuration;
        _player.Velocity = _player.InputVector * _player.DashSpeed;
        _player.DashCooldownTimer = _player.DashCooldown;

        // направление рывка
        string dashTrigger;
        if (_player.InputVector.y > 0.5f)
            dashTrigger = "isDashBack";
        else if (_player.InputVector.y < -0.5f)
            dashTrigger = "isDashFront";
        else
            dashTrigger = "isDashSide";

        // проверить, что PlayerVisual существует
        var playerVisual = Player.Instance.GetComponentInChildren<PlayerVisual>();
        if (playerVisual != null) {
            playerVisual.TriggerDashAnimation(dashTrigger);
        }
        else {
            Debug.LogWarning("PlayerVisual component not found on Player!");
        }

        Debug.Log("Player entered Dashing state");
    }

    public void Update(float deltaTime) {
        _player.UpdateInput();
        _dashTimeLeft -= deltaTime;
        if (_dashTimeLeft <= 0) {
            _player.ChangeState(_player.InputVector.magnitude > 0 ? new PlayerMovingState(_player) : new PlayerIdleState(_player));
        }
    }

    public void Exit() {
        _player.Velocity = Vector2.zero;
    }
}
