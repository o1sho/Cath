using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour, IPlayerComponent {
    [Header("Movement Settings")]
    [SerializeField] private float maxMoveSpeed = 5f;

    private Player _player;
    private Rigidbody2D _rigidbody;
    private Vector2 _inputVector;
    private Vector2 _velocity;
    private Vector2 _lastNonZeroInput = Vector2.down;

    public Vector2 InputVector => _inputVector;
    public Vector2 Velocity => _velocity;
    public Vector2 LastNonZeroInput => _lastNonZeroInput;

    public enum MovementMode {
        Input, // Обычное движение по input
        Override, // Dash, Knockback и т.п.
        Frozen // Полная остановка
    }
    private MovementMode _currentMode = MovementMode.Input;
    private Vector2 _overrideVelocity;

    //For riding on platform
    private bool _isOnRidingSurface = false;
    private Transform _rideTarget;
    private Vector3 _lastRideTargetPosition;

    //---------------
    public void Init(Player player) {
        _player = player;
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_rigidbody == null)
            Debug.LogError("PlayerMovementHandler: Rigidbody2D not found!");
    }
    //---------------

    public void UpdateInput() {
        Vector2 rawInput = GameInput.Instance.GetMovementVector();
        _inputVector = GetOrthogonalInput(rawInput);

        if (_inputVector.magnitude > 0) {
            _lastNonZeroInput = _inputVector;
        }
    }

    private Vector2 GetOrthogonalInput(Vector2 rawInput) {
        if (rawInput.magnitude == 0)
            return Vector2.zero;

        if (Mathf.Abs(rawInput.x) > Mathf.Abs(rawInput.y)) {
            return Mathf.Approximately(rawInput.x, 0) ? Vector2.zero : new Vector2(Mathf.Sign(rawInput.x), 0);
        }
        else {
            return Mathf.Approximately(rawInput.y, 0) ? Vector2.zero : new Vector2(0, Mathf.Sign(rawInput.y));
        }
    }

    public void SetMovementMode(MovementMode mode) {
        if (_currentMode != mode) {
            Debug.Log($"Movement mode changed: {_currentMode} -> {mode}");
            _currentMode = mode;
        }
    }

    public void SetOverrideVelocity(Vector2 velocity) {
        _overrideVelocity = velocity;
        _currentMode = MovementMode.Override;
    }

    public void ResetToInputControl() {
        _currentMode = MovementMode.Input;
    }

    public void AttachToPlatform(Transform rideTarget) {
        _isOnRidingSurface = true;
        _rideTarget = rideTarget;
        _lastRideTargetPosition = rideTarget.position;
    }

    public void DetachFromPlatform() {
        _isOnRidingSurface = false;
        _rideTarget = null;
    }

    public void Stop() {
        _velocity = Vector2.zero;
        _inputVector = Vector2.zero;
        _rigidbody.linearVelocity = Vector2.zero;
    }

    public void ResetVelocity() {
        _velocity = Vector2.zero;
        _inputVector = Vector2.zero;
        _rigidbody.linearVelocity = Vector2.zero;
    }

    private void FixedUpdate() {
        Vector2 finalVelocity = Vector2.zero;

        switch (_currentMode) {
            case MovementMode.Input:
                finalVelocity = _inputVector * maxMoveSpeed;
                break;

            case MovementMode.Override:
                finalVelocity = _overrideVelocity;
                break;

            case MovementMode.Frozen:
                finalVelocity = Vector2.zero;
                break;
        }

        if (_isOnRidingSurface && _rideTarget != null) {
            Vector2 platformVelocity = (_rideTarget.position - _lastRideTargetPosition) / Time.fixedDeltaTime;
            finalVelocity += platformVelocity;
            _lastRideTargetPosition = _rideTarget.position;
        }

        _velocity = finalVelocity;
        _rigidbody.linearVelocity = finalVelocity;
    }
}
