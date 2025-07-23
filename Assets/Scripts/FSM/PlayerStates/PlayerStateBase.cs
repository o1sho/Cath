using UnityEngine;

public abstract class PlayerStateBase : IState {
    protected readonly Player _player;
    protected readonly PlayerVisualHandler _visual;

    protected PlayerStateBase(Player player) {
        _player = player;
        _visual = Player.Instance.GetComponentInChildren<PlayerVisualHandler>();
    }

    public abstract void Enter();
    public abstract void Update(float deltaTime);
    public abstract void Exit();
}