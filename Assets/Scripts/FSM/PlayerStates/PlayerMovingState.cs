using UnityEngine;

public class PlayerMovingState : IState 
{
    private readonly Player _player;
    private readonly PlayerVisual _visual;

    public PlayerMovingState(Player player) {
        _player = player;
        _visual = Player.Instance.GetComponentInChildren<PlayerVisual>();
    }

    public void Enter() {
        _visual.SetLocomotionState(isMoving: true, Mathf.Abs(_player.Movement.InputVector.x), _player.Movement.InputVector.y);
        _visual.SetFacingDirection(_player.Movement.InputVector);

        Debug.Log("Player entered Moving state");
    }

    public void Update(float deltaTime) {
        _player.Movement.UpdateInput();

        if (_player.Movement.InputVector.magnitude == 0) {
            _player.ChangeState(new PlayerIdleState(_player));
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

        if (GameInput.Instance.InputSystem.Player.Interact.WasPressedThisFrame()) {
            _player.ChangeState(new PlayerPickupState(_player));
        }

        _player.Movement.Move(deltaTime);

        _visual.SetLocomotionState(isMoving: true, Mathf.Abs(_player.Movement.InputVector.x), _player.Movement.InputVector.y);
        _visual.SetFacingDirection(_player.Movement.InputVector);

        _player.Dash.UpdateCooldown(deltaTime);
    }

    public void Exit() {
    }


}
