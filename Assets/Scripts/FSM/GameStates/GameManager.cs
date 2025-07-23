using System;
using UnityEngine.SceneManagement;

public class GameManager : StateMachine
{
    public static GameManager Instance { get; private set; }

    private GamePlayingState _playingState;
    private GamePausedState _pausedState;
    private GameMainMenuState _mainMenuState;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            _playingState = new GamePlayingState(this);
            _pausedState = new GamePausedState(this);
            _mainMenuState = new GameMainMenuState(this);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "Gameplay") {
            ChangeState(_playingState);
        }
        else if (scene.name == "MainMenu") {
            ChangeState(_mainMenuState);
        }
    }

    public void PlayGame() => ChangeState(_playingState);
    public void PauseGame() => ChangeState(_pausedState);
    public void ResumeGame() => ChangeState(_playingState);
    public void ToMainMenu() => ChangeState(_mainMenuState);
}
