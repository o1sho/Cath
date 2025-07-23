using UnityEngine;

public class PlayerDashingState : PlayerStateBase {
    public PlayerDashingState(Player player) : base(player) { }

    private float _dashTimeLeft;

    public override void Enter() {
        _dashTimeLeft = _player.Dash.DashDuration;
        _player.Dash.TriggerCooldown();
        _player.Movement.SetOverrideVelocity(_player.Movement.InputVector * _player.Dash.GetDashSpeed());
        _player.Movement.SetMovementMode(PlayerMovementHandler.MovementMode.Override);

        string dashTrigger = _player.Movement.InputVector.y > 0.5f ? "isDashBack" :
                            _player.Movement.InputVector.y < -0.5f ? "isDashFront" : "isDashSide";

        _visual.TriggerDashAnimation(dashTrigger);
        _visual.SetFacingDirection(_player.Movement.InputVector);

        Debug.Log("Player entered Dashing state");
    }

    public override void Update(float deltaTime) {
        _player.Movement.UpdateInput();

        _dashTimeLeft -= deltaTime;
        if (_dashTimeLeft <= 0) {
            _player.ChangeState(_player.Movement.InputVector.magnitude > 0 ? _player.MovingState : _player.IdleState);
        }
    }

    public override void Exit() {
        _player.Movement.ResetToInputControl();
    }
}
