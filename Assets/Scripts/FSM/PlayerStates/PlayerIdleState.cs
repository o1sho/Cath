using UnityEngine;

public class PlayerIdleState : PlayerStateBase {
    public PlayerIdleState(Player player) : base(player) { }

    public override void Enter() {
        _player.Movement.SetMovementMode(PlayerMovementHandler.MovementMode.Frozen);
        _player?.Movement.Stop();

        _visual.SetLocomotionState(isMoving: false, Mathf.Abs(_player.Movement.LastNonZeroInput.x), _player.Movement.LastNonZeroInput.y);
        _visual.SetFacingDirection(_player.Movement.LastNonZeroInput);

        Debug.Log("Player entered Idle state");
    }

    public override void Update(float deltaTime) {
        _player.Movement.UpdateInput();

        if (_player.Movement.InputVector.magnitude > 0) {
            _player.ChangeState(_player.MovingState);
            return;
        }

        if (_player.Abilities.Can(Ability.Dash) && _player.Dash.CanDash && GameInput.Instance.InputSystem.Player.Dash.WasPressedThisFrame() && _player.Movement.InputVector.magnitude > 0) {
            _player.ChangeState(_player.DashingState);
            return;
        }

        if (_player.Throw.HeldItem != null && GameInput.Instance.InputSystem.Player.Throw.WasPressedThisFrame()) {
            _player.ChangeState(_player.ThrowingState);
            return;
        }

        if (!_player.GroundCheck.IsGround && !_player.GroundCheck.IsGroundForRiding) {
            _player.ChangeState(_player.FallState);
            return;
        }

        if (GameInput.Instance.InputSystem.Player.Interact.WasPressedThisFrame()) {
            _player.ChangeState(_player.PickupState);
        }

        _player.Dash.UpdateCooldown(deltaTime);
    }

    public override void Exit() {
        _player.Movement.ResetToInputControl();
    }
}
