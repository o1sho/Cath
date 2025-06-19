using UnityEngine;

public class GamePausedState : IState {

    private readonly GameManager _manager;

    public GamePausedState(GameManager manager) {
        _manager = manager;
    }

    public void Enter() {
        Time.timeScale = 0f;
        Debug.Log("Entered Paused state");
    }

    public void Exit() {
        
    }

    public void Update(float deltaTime) {
        
    }
}
