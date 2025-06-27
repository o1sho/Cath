using UnityEngine;

public class PlayerDashingState : IState {
    private readonly Player _player;
    private readonly PlayerVisual _visual;

    private float _dashTimeLeft;

    public PlayerDashingState(Player player) {
        _player = player;
        _visual = Player.Instance.GetComponentInChildren<PlayerVisual>();
    }

    public void Enter() {
        _dashTimeLeft = _player.Dash.DashDuration;
        _player.Dash.StartDash(_player.Movement.InputVector, _player.Movement);

        string dashTrigger = _player.Movement.InputVector.y > 0.5f ? "isDashBack" :
                            _player.Movement.InputVector.y < -0.5f ? "isDashFront" : "isDashSide";

        _visual.TriggerDashAnimation(dashTrigger);
        _visual.SetFacingDirection(_player.Movement.InputVector);

        Debug.Log("Player entered Dashing state");
    }

    public void Update(float deltaTime) {
        _player.Movement.UpdateInput();
        _dashTimeLeft -= deltaTime;
        if (_dashTimeLeft <= 0) {
            _player.ChangeState(_player.Movement.InputVector.magnitude > 0 ? new PlayerMovingState(_player) : new PlayerIdleState(_player));
        }
    }

    public void Exit() {
        _player.Movement.Stop();
    }
}
