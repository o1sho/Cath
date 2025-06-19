using UnityEngine;

public class PlayerMovingState : IState 
{
    private readonly Player _player;

    public PlayerMovingState(Player player) {
        _player = player;
    }

    public void Enter() {
        Debug.Log("Player entered Moving state");
    }

    public void Update(float deltaTime) {
        _player.UpdateInput();

        // ������� � Idle
        if (_player.InputVector.magnitude == 0) {
            _player.ChangeState(new PlayerIdleState(_player));
            return;
        }

        // ������� � Dashing
        if (_player.DashCooldownTimer <= 0 && GameInput.Instance.InputSystem.Player.Dash.WasPressedThisFrame() && _player.InputVector.magnitude > 0) {
            _player.ChangeState(new PlayerDashingState(_player));
            return;
        }

        // ��������
        Vector2 desiredVelocity = _player.InputVector * _player.MaxMoveSpeed;
        _player.Velocity = Vector2.MoveTowards(_player.Velocity, desiredVelocity, _player.Acceleration * deltaTime);

        // ���������� ������� �����
        if (_player.DashCooldownTimer > 0) {
            _player.DashCooldownTimer -= deltaTime;
        }
    }

    public void Exit() { }

}
