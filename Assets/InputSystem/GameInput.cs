using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private InputSystem_Actions _inputSystem;

    private void Awake() {
        Instance = this;

        _inputSystem = new InputSystem_Actions();
        _inputSystem.Enable();
    }

    public Vector2 GetMovementVector() {
        Vector2 inputVector = _inputSystem.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }
}
