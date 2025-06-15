using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [Header("Movement Settings:")]
    [SerializeField] private float maxMoveSpeed = 5f;
    [SerializeField] private float acceleration = 50f;

    [Header("Dash Settings:")]
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    private Vector2 _inputVector;
    private Vector2 _velocity;
    private Rigidbody2D _rigidbody;
    private Vector3 _currentSpawnPoint;

    private bool _isDashing;
    private float _dashTimeLeft;
    private float _dashCooldownTimer;

    public Rigidbody2D Rigidbody => _rigidbody;
    public Vector3 CurrentSpawnPoint;


    private void Awake() {
        Instance = this;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start() {

    }

    private void Update() {
        _inputVector = GameInput.Instance.GetMovementVector();

        if (!_isDashing && _dashCooldownTimer <= 0 && GameInput.Instance.InputSystem.Player.Dash.WasPressedThisFrame()) {
            HandleDash();
        }

        UpdateDashTimer();
    }

    private void FixedUpdate() {
        
        HandleMovement();
        _rigidbody.linearVelocity = _velocity;
    }

    private void HandleMovement() {
        Vector2 desiredVelocity = _inputVector * maxMoveSpeed;

        if (_inputVector.magnitude > 0) {
            _velocity = Vector2.MoveTowards(_velocity, desiredVelocity, acceleration * Time.fixedDeltaTime);
        }
        else {
            _velocity = Vector2.MoveTowards(_velocity, Vector2.zero, acceleration * Time.fixedDeltaTime);
        }
    }

    //
    private void HandleDash() {
        if (_inputVector.magnitude > 0) {
            _isDashing = true;
            _dashTimeLeft = dashDuration;
            _dashCooldownTimer = dashCooldown;
            _velocity = _inputVector * dashSpeed;
        }
    }

    private void UpdateDashTimer() {
        if (_isDashing) {
            _dashTimeLeft -= Time.deltaTime;
            if (_dashTimeLeft <= 0) {
                _isDashing = false;
            }
        }

        if (_dashCooldownTimer > 0) {
            _dashCooldownTimer -= Time.deltaTime;
        }
    }
    //

    public void Respawn() {
        transform.position = CurrentSpawnPoint;
    }

}
