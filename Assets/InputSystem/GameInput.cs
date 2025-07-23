using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private InputSystem_Actions _inputSystem;

    public InputSystem_Actions InputSystem => _inputSystem;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }



        _inputSystem = new InputSystem_Actions();
        _inputSystem.Enable();
    }

    public Vector2 GetMovementVector() {
        Vector2 inputVector = _inputSystem.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    void OnDisable() {
        _inputSystem.Disable();
    }
}
