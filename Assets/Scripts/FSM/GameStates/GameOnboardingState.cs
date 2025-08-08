using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOnboardingState : IState
{
    private readonly GameManager _manager;

    public GameOnboardingState(GameManager manager) {
        _manager = manager;
    }

    public void Enter() {
        Time.timeScale = 1f;

        Debug.Log("Entered Onboarding state");
        UIManager.Instance.ShowHUD();

        if (SceneManager.GetActiveScene().name != "1_Onboarding") {
            SceneManager.LoadScene("1_Onboarding");
        }
    }

    public void Update(float deltaTime) {
        if (GameInput.Instance.InputSystem.Player.Pause.WasPressedThisFrame()) {
            _manager.PauseGame();
        }
    }
    public void Exit() { }
}
