using UnityEngine;

public class GameplayUIEvents : MonoBehaviour {
    public void OnPausePressed() {
        GameManager.Instance?.PauseGame();
    }

    // Можешь сюда добавить что-то вроде:
    public void OnInventoryPressed() {
        Debug.Log("Inventory not implemented yet.");
    }
}