using UnityEngine;

public class PlayerFallState : IState
{
    private readonly Player _player;
    private readonly PlayerVisual _visual;

    private float _fallTimer = 0.8f;

    public PlayerFallState(Player player) {
        _player = player;
        _visual = Player.Instance.GetComponentInChildren<PlayerVisual>();
    }

    public void Enter() {
        _player.Movement.Stop();
        _visual.TriggerAnimation("isFall");

        Debug.Log("Player entered Fall state");
    }

    public void Update(float deltaTime) {
        _fallTimer -= deltaTime;
        if (_fallTimer < 0) {
            _player.Respawn();
            _player.ChangeState(new PlayerIdleState(_player));
        }
    }

    public void Exit() {
    }
}
