using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : StateMachine
{
    public static GameManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    private void OnEnable() {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Gameplay") {
            ChangeState(new GamePlayingState(this));
        }
        else {
            ChangeState(new GameMainMenuState(this));
        }
    }
}
