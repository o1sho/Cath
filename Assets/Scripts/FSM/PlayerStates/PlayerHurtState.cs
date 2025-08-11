using UnityEngine;

public class PlayerHurtState : PlayerStateBase
{
    private readonly DamageInfo _info;
    private float _timer;


    public PlayerHurtState(Player player) : base(player) { }

    public override void Enter() {
        var info = _player.Health.LastDamage;

        _player?.Movement.SetMovementMode(PlayerMovementHandler.MovementMode.Frozen);
        _player?.Movement.Stop();

        _visual?.SetLocomotionState(isMoving: false, Mathf.Abs(_player.Movement.LastNonZeroInput.x), _player.Movement.LastNonZeroInput.y);
        _visual?.SetFacingDirection(_player.Movement.LastNonZeroInput);

        _timer = Mathf.Max(0.05f, info.HitStunDuration);

        Debug.Log("Player entered Hurt state");
    }

    public override void Exit() {
        _player.Movement.ResetToInputControl();
    }

    public override void Update(float deltaTime) {
        _timer -= Time.deltaTime;

        if (_timer <= 0f && _player.Health.CurrentHealth > 0) {
            _player.ChangeState(_player.IdleState);
        }
        else if (_timer <= 0f && _player.Health.CurrentHealth <= 0) {
            _player.ChangeState(_player.DeadState);
        }
    }
}
