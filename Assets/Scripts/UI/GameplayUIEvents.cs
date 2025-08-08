using UnityEngine;

public class GameplayUIEvents : MonoBehaviour {
    public void OnPausePressed() {
        GameManager.Instance?.PauseGame();
    }

    public void OnInventoryPressed() {
        Debug.Log("Inventory not implemented yet.");
    }
}