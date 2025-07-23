using UnityEngine;

public class PauseUIEvents : MonoBehaviour {
    public void OnResumePressed() {
        GameManager.Instance?.ResumeGame();
    }

    public void OnMainMenuPressed() {
        GameManager.Instance?.ToMainMenu();
    }
}