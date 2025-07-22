using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float maxMoveSpeed = 5f;
    [SerializeField] private float acceleration = 50f;

    private Rigidbody2D _rb;
    private Vector2 _inputVector;
    private Vector2 _velocity;
    private Vector2 _lastNonZeroInput;

    public Vector2 InputVector => _inputVector;
    public Vector2 Velocity => _velocity;
    public Vector2 LastNonZeroInput => _lastNonZeroInput;

    // --- Platform Riding ---
    private bool _isOnRidingSurface = false;
    private Transform _rideTarget;
    private Vector3 _lastRideTargetPosition;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _lastNonZeroInput = Vector2.down; 
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
    public void SetVelocity(Vector2 velocity) => _velocity = velocity;

    public void Stop() {
        _velocity = Vector2.zero;
        if (_rb != null) _rb.linearVelocity = Vector2.zero;
    }

    public void ResetVelocity() {
        _velocity = Vector2.zero;
        _inputVector = Vector2.zero;
        _rb.linearVelocity = Vector2.zero;
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


    public void AttachToPlatform(Transform rideTarget) {
        _isOnRidingSurface = true;
        _rideTarget = rideTarget;
        _lastRideTargetPosition = rideTarget.position;
    }

    public void DetachFromPlatform() {
        _isOnRidingSurface = false;
        _rideTarget = null;
    }

    private void FixedUpdate() {
        Vector2 finalVelocity = _velocity;

        if (_isOnRidingSurface && _rideTarget != null) {
            Vector2 platformVelocity = (_rideTarget.position - _lastRideTargetPosition) / Time.fixedDeltaTime;
            finalVelocity += platformVelocity;
            _lastRideTargetPosition = _rideTarget.position;
        }

        _rb.linearVelocity = finalVelocity;
    }
}
