using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayingState : IState
{
    private readonly GameManager _manager;

    public GamePlayingState(GameManager manager) {
        _manager = manager;
    }

    public void Enter() {
        Time.timeScale = 1f;

        Debug.Log("Entered Gameplaying state");
        UIManager.Instance.ShowHUD();

        if (SceneManager.GetActiveScene().name != "2_MainGameplay") {
            SceneManager.LoadScene("2_MainGameplay");
        }
    }

    public void Update(float deltaTime) {
        if (GameInput.Instance.InputSystem.Player.Pause.WasPressedThisFrame()) {
            _manager.PauseGame();
        }
    }
    public void Exit() { }
}
