using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private string playWord = "play";

    private void Update() {
        if (inputField.text.ToLower() == playWord.ToLower() && GameInput.Instance.InputSystem.Player.Play.WasPressedThisFrame()) {
            
            GameManager.Instance.ChangeState(new GamePlayingState(GameManager.Instance));
        }
    }
}
