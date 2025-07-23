using UnityEngine;

public class PlayerDeadState : PlayerStateBase {
    public PlayerDeadState(Player player) : base(player) { }

    public override void Enter() {
        _player.Movement.Stop();
        _visual.SetDeadState(isDead: true);
        Debug.Log("Player entered Dead state");
        GameManager.Instance.ChangeState(new GameMainMenuState(GameManager.Instance));
    }

    public override void Update(float deltaTime) {
        if (GameInput.Instance.InputSystem.Player.Respawn.WasPressedThisFrame()) {
            _player.Respawn();
        }
    }

    public override void Exit() {
    }
}


