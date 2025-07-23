using UnityEngine;

public class PlayerFallState : PlayerStateBase {
    public PlayerFallState(Player player) : base(player) { }

    private float _fallTimer = 0.8f;

    public override void Enter() {
        Debug.Log("Player entered Fall state");

        _player.Movement.SetMovementMode(PlayerMovementHandler.MovementMode.Frozen);
        _player.Movement.Stop();
        _visual.TriggerAnimation("isFall");
    }

    public override void Update(float deltaTime) {
        _fallTimer -= deltaTime;
        if (_fallTimer < 0) {
            _player.Respawn();
            _player.ChangeState(_player.IdleState);
        }
    }

    public override void Exit() {
        _player.Movement.ResetToInputControl();
    }
}
