using UnityEngine;

public class PlayerDeadState : IState {
    private readonly Player _player;

    public PlayerDeadState(Player player) {
        _player = player;
    }

    public void Enter() {
        _player.Velocity = Vector2.zero;
        Debug.Log("Player entered Dead state");
        GameManager.Instance.ChangeState(new GameMainMenuState(GameManager.Instance));
    }

    public void Update(float deltaTime) {
        if (GameInput.Instance.InputSystem.Player.Respawn.WasPressedThisFrame()) {
            _player.Respawn();
        }
    }

    public void Exit() { }
}
