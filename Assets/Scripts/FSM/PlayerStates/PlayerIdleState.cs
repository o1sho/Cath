using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerIdleState : IState {
    private readonly Player _player;

    public PlayerIdleState(Player player) {
        _player = player;
    }

    public void Enter() {
        _player.Velocity = Vector2.zero;
        Debug.Log("Player entered Idle state");
    }

    public void Update(float deltaTime) {
        _player.UpdateInput();

        // Переход в Moving
        if (_player.InputVector.magnitude > 0) {
            _player.ChangeState(new PlayerMovingState(_player));
            return;
        }

        // Переход в Dashing
        if (_player.DashCooldownTimer <= 0 && GameInput.Instance.InputSystem.Player.Dash.WasPressedThisFrame() && _player.InputVector.magnitude > 0) {
            _player.ChangeState(new PlayerDashingState(_player));
            return;
        }

        // Замедление
        _player.Velocity = Vector2.MoveTowards(_player.Velocity, Vector2.zero, _player.Acceleration * deltaTime);

        // Обновление таймера рывка
        if (_player.DashCooldownTimer > 0) {
            _player.DashCooldownTimer -= deltaTime;
        }
    }

    public void Exit() { }
}
