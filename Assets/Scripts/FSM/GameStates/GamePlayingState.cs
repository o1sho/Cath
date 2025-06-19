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
        Debug.Log("Entered Playing state");

        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Gameplay") {
            return;
        }
        else {
            SceneManager.LoadScene("Gameplay");
        }
    }

    public void Update(float deltaTime) {

    }

    public void Exit() { }
}
