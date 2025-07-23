using UnityEngine;

public class GamePausedState : IState {

    private readonly GameManager _manager;

    public GamePausedState(GameManager manager) {
        _manager = manager;
    }

    public void Enter() {
        Time.timeScale = 0f;
        UIManager.Instance.ShowPauseMenu();
        Debug.Log("Entered Paused state");
    }

    public void Exit() {
        UIManager.Instance.HidePauseMenu();
    }

    public void Update(float deltaTime) {
        if (GameInput.Instance.InputSystem.Player.Pause.WasPressedThisFrame()) {
            _manager.ResumeGame();
        }
    }
}
