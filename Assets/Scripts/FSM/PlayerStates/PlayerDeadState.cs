using UnityEngine;

public class PlayerDeadState : PlayerStateBase {
    public PlayerDeadState(Player player) : base(player) { }

    public override void Enter() {
        _player.Movement.SetMovementMode(PlayerMovementHandler.MovementMode.Frozen);
        _player?.Movement.Stop();

        _visual.SetDeadState(isDead: true);

        Debug.Log("Player entered Dead state");

        _player.Respawn();
        //GameManager.Instance.ReloadActiveScene();
    }

    public override void Update(float deltaTime) {
        //if (GameInput.Instance.InputSystem.Player.Respawn.WasPressedThisFrame()) {
        //    _player.Respawn();
        //}
    }

    public override void Exit() {
        _player.Movement.ResetToInputControl();
    }
}


