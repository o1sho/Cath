using UnityEngine;

public class PlayerDeadState : IState {
    private readonly Player _player;
    private readonly PlayerVisual _visual;

    public PlayerDeadState(Player player) {
        _player = player;
        _visual = Player.Instance.GetComponentInChildren<PlayerVisual>();
    }

    public void Enter() {
        _player.Movement.Stop();
        _visual.SetDeadState(isDead: true);
        Debug.Log("Player entered Dead state");
        GameManager.Instance.ChangeState(new GameMainMenuState(GameManager.Instance));
    }

    public void Update(float deltaTime) {
        if (GameInput.Instance.InputSystem.Player.Respawn.WasPressedThisFrame()) {
            _player.Respawn();
        }
    }

    public void Exit() {
    }
}


