using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMainMenuState : IState {

    private readonly GameManager _manager;

    public GameMainMenuState(GameManager manager) {
        _manager = manager;
    }

    public void Enter() {
        Time.timeScale = 0f;
        Debug.Log("Entered MainMenu state");

        if (SceneManager.GetActiveScene().name != "MainMenu") {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void Exit() { }
    public void Update(float deltaTime) { }
}
