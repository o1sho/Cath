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

        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "MainMenu") {
            return;
        }
        else {
            SceneManager.LoadScene("MainMenu");
        }

    }

    public void Exit() {
        
    }

    public void Update(float deltaTime) {
        
    }

}
