using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Panels")]
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject pausePanel;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void ShowHUD() {
        hudPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void ShowPauseMenu() {
        pausePanel.SetActive(true);
    }

    public void HidePauseMenu() {
        pausePanel.SetActive(false);
    }

    public void TogglePauseMenu() {
        pausePanel.SetActive(!pausePanel.activeSelf);
    }
}
