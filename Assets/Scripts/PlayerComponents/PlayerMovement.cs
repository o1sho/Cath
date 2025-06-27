using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxMoveSpeed = 5f;
    [SerializeField] private float acceleration = 50f;

    private Vector2 _inputVector;
    private Vector2 _velocity;
    private Vector2 _lastNonZeroInput;

    public Vector2 InputVector => _inputVector;
    public Vector2 Velocity => _velocity;
    public Vector2 LastNonZeroInput => _lastNonZeroInput;

    private void Awake() {
        _lastNonZeroInput = Vector2.down; // По умолчанию вниз
    }

    public void UpdateInput() {
        Vector2 rawInput = GameInput.Instance.GetMovementVector();
        _inputVector = GetOrthogonalInput(rawInput);
        if (_inputVector.magnitude > 0) {
            _lastNonZeroInput = _inputVector;
        }
    }

    public void Move(float deltaTime) {
        Vector2 desiredVelocity = _inputVector * maxMoveSpeed;
        _velocity = Vector2.MoveTowards(_velocity, desiredVelocity, acceleration * deltaTime);
    }
    public void SetVelocity(Vector2 velocity) {
        _velocity = velocity;
    }

    public void Stop() {
        _velocity = Vector2.zero;
    }

    public void ResetVelocity() {
        _velocity = Vector2.zero;
        _inputVector = Vector2.zero;
    }

    private Vector2 GetOrthogonalInput(Vector2 rawInput) {
        if (rawInput.magnitude == 0)
            return Vector2.zero;

        if (Mathf.Abs(rawInput.x) > Mathf.Abs(rawInput.y)) {
            return new Vector2(Mathf.Sign(rawInput.x), 0);
        }
        else {
            return new Vector2(0, Mathf.Sign(rawInput.y));
        }
    }
}
