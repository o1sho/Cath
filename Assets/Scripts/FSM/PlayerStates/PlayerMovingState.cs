using UnityEngine;

public class PlayerMovingState : PlayerStateBase 
{
    public PlayerMovingState(Player player) : base(player) { }

    public override void Enter() {

        _visual.SetLocomotionState(isMoving: true, Mathf.Abs(_player.Movement.InputVector.x), _player.Movement.InputVector.y);
        _visual.SetFacingDirection(_player.Movement.InputVector);

        Debug.Log("Player entered Moving state");
    }

    public override void Update(float deltaTime) {
        _player.Movement.UpdateInput();

        if (_player.Movement.InputVector.magnitude == 0) {
            _player.ChangeState(_player.IdleState);
            return;
        }

        if (_player.Dash.CanDash && GameInput.Instance.InputSystem.Player.Dash.WasPressedThisFrame() && _player.Movement.InputVector.magnitude > 0) {
            _player.ChangeState(_player.DashingState);
            return;
        }

        if (_player.Throw.HeldItem != null && GameInput.Instance.InputSystem.Player.Throw.WasPressedThisFrame()) {
            _player.ChangeState(_player.ThrowingState);
            return;
        }

        if (GameInput.Instance.InputSystem.Player.Interact.WasPressedThisFrame()) {
            _player.ChangeState(_player.PickupState);
        }

        if (!_player.GroundCheck.IsGround && !_player.GroundCheck.IsGroundForRiding) {
            _player.ChangeState(_player.FallState);
            return;
        }

        _visual.SetLocomotionState(isMoving: true, Mathf.Abs(_player.Movement.InputVector.x), _player.Movement.InputVector.y);
        _visual.SetFacingDirection(_player.Movement.InputVector);

        _player.Dash.UpdateCooldown(deltaTime);
    }

    public override void Exit() {
        _player.Movement.ResetToInputControl();
    }


}
