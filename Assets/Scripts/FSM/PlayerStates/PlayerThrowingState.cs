using UnityEngine;

public class PlayerThrowingState : IState {
    private readonly Player _player;
    private readonly PlayerVisual _visual;
    private float _throwTimeLeft;

    public PlayerThrowingState(Player player) {
        _player = player;
        _visual = Player.Instance.GetComponentInChildren<PlayerVisual>();
        _throwTimeLeft = 0.2f; // Длительность анимации броска
    }

    public void Enter() {
        _player.Movement.Stop();
        Vector2 direction = _player.Movement.InputVector.magnitude > 0 ? _player.Movement.InputVector : _player.Movement.LastNonZeroInput;
        _player.Throw.ThrowItem(direction);

        _visual.TriggerThrowAnimation("isThrowing");
        _visual.SetFacingDirection(direction);

        Debug.Log("Player entered Throwing state");
    }

    public void Update(float deltaTime) {
        _player.Movement.UpdateInput();
        _throwTimeLeft -= deltaTime;
        if (_throwTimeLeft <= 0) {
            _player.ChangeState(_player.Movement.InputVector.magnitude > 0 ? new PlayerMovingState(_player) : new PlayerIdleState(_player));
        }
    }

    public void Exit() {
        _player.Movement.Stop();
    }
}
