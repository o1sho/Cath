using System;
using UnityEngine.SceneManagement;

public class GameManager : StateMachine
{
    public static GameManager Instance { get; private set; }

    private GameOnboardingState _onboardingState;
    private GamePlayingState _playingState;
    private GamePausedState _pausedState;
    private GameMainMenuState _mainMenuState;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            _onboardingState = new GameOnboardingState(this);
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
        if (scene.name == "0_MainMenu") {
            ChangeState(_mainMenuState);
        }
        else if (scene.name == "1_Onboarding") {
            ChangeState(_onboardingState);
        }
        else if (scene.name == "2_MainGameplay") {
            ChangeState(_playingState);
        }
    }

    public void PlayGame() => ChangeState(_onboardingState);
    public void PauseGame() => ChangeState(_pausedState);
    public void ResumeGame() => ChangeState(_onboardingState);
    public void ToMainMenu() => ChangeState(_mainMenuState);
}
