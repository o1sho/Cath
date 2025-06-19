using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject pausePanel;

    private void Start() {
        pausePanel.SetActive(false);
    }

    private void Update() {
        timerText.text = PlayerSpawnController.GetRespawnTimeInSeconds().ToString();

        if (GameInput.Instance.InputSystem.Player.Pause.WasPressedThisFrame()) {

            GameManager.Instance.ChangeState(new GamePausedState(GameManager.Instance));
            pausePanel.SetActive(true);
        }
    }

    public void HandleMenu() {
        if (pausePanel.activeSelf == true) {
            pausePanel.SetActive(false);
            GameManager.Instance.ChangeState(new GamePlayingState(GameManager.Instance));
        }
        else {
            pausePanel.SetActive(true);
            GameManager.Instance.ChangeState(new GamePausedState(GameManager.Instance));
        }
    }

    public void HandleSettings() {
        Debug.Log("Settings is open");
    }

    public void HandleMainMenu() {
        GameManager.Instance.ChangeState(new GameMainMenuState(GameManager.Instance));
    }
}
