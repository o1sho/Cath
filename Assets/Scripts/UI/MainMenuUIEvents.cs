using UnityEngine;

public class MainMenuUIEvents : MonoBehaviour {
    public void OnPlayPressed() {
        GameManager.Instance?.PlayGame();
    }

    public void OnQuitPressed() {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}