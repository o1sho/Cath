using UnityEngine;

public class PlayerIdleState : IState {
    private readonly Player _player;
    private readonly PlayerVisual _visual;

    public PlayerIdleState(Player player) {
        _player = player;
        _visual = Player.Instance.GetComponentInChildren<PlayerVisual>();
    }

    public void Enter() {
        _player.Movement.Stop();

        _visual.SetLocomotionState(isMoving: false, Mathf.Abs(_player.Movement.LastNonZeroInput.x), _player.Movement.LastNonZeroInput.y);
        _visual.SetFacingDirection(_player.Movement.LastNonZeroInput);

        Debug.Log("Player entered Idle state");
    }

    public void Update(float deltaTime) {
        _player.Movement.UpdateInput();

        if (_player.Movement.InputVector.magnitude > 0) {
            _player.ChangeState(new PlayerMovingState(_player));
            return;
        }

        if (_player.Dash.CanDash && GameInput.Instance.InputSystem.Player.Dash.WasPressedThisFrame() && _player.Movement.InputVector.magnitude > 0) {
            _player.ChangeState(new PlayerDashingState(_player));
            return;
        }

        if (_player.Throw.HeldItem != null && GameInput.Instance.InputSystem.Player.Throw.WasPressedThisFrame()) {
            _player.ChangeState(new PlayerThrowingState(_player));
            return;
        }

        if (!_player.GroundCheck.IsGround && !_player.GroundCheck.IsGroundForRiding) {
            _player.ChangeState(new PlayerFallState(_player));
            return;
        }


        if (GameInput.Instance.InputSystem.Player.Interact.WasPressedThisFrame()) {
            _player.ChangeState(new PlayerPickupState(_player));
        }

        _player.Dash.UpdateCooldown(deltaTime);
    }

    public void Exit() {

    }


}
