using UnityEngine;

public class PlayerThrowingState : PlayerStateBase {

    public PlayerThrowingState(Player player) : base(player) { }

    private float _throwTimeLeft = 0.2f;

    public override void Enter() {
        Debug.Log("Player entered Throwing state");

        _player.Movement.SetMovementMode(PlayerMovementHandler.MovementMode.Frozen);
        _player.Movement.Stop();

        Vector2 direction = _player.Movement.InputVector.magnitude > 0 ? _player.Movement.InputVector : _player.Movement.LastNonZeroInput;
        _player.Throw.ThrowItem(direction);

        _visual.TriggerThrowAnimation("isThrowing");
        _visual.SetFacingDirection(direction);
    }

    public override void Update(float deltaTime) {
        _player.Movement.UpdateInput();
        _throwTimeLeft -= deltaTime;
        if (_throwTimeLeft <= 0) {
            _player.ChangeState(_player.Movement.InputVector.magnitude > 0 ? _player.MovingState : _player.IdleState);
        }
    }

    public override void Exit() {
        _player.Movement.ResetToInputControl();
    }
}
