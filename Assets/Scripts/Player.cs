using UnityEngine;

public class Player : StateMachine
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

    [HideInInspector] public Rigidbody2D Rigidbody => _rigidbody;
    public Vector3 CurrentSpawnPoint { get => _currentSpawnPoint; set => _currentSpawnPoint = value; }
    public Vector2 InputVector { get => _inputVector; private set => _inputVector = value; }
    public Vector2 Velocity { get => _velocity; set => _velocity = value; }
    public float DashCooldownTimer { get; set; }

    public float MaxMoveSpeed => maxMoveSpeed;
    public float Acceleration => acceleration;
    public float DashSpeed => dashSpeed;
    public float DashDuration => dashDuration;
    public float DashCooldown => dashCooldown;


    private void Awake() {
        Instance = this;
        _rigidbody = GetComponent<Rigidbody2D>();

        ChangeState(new PlayerIdleState(this));
    }


    private void FixedUpdate() {
        _rigidbody.linearVelocity = _velocity; // Применяем скорость
    }

    public void Respawn() {
        transform.position = _currentSpawnPoint;
        _velocity = Vector2.zero;
        DashCooldownTimer = 0f;
        ChangeState(new PlayerIdleState(this));
    }

    public void UpdateInput() {
        Vector2 rawInput = GameInput.Instance.GetMovementVector();
        _inputVector = GetOrthogonalInput(rawInput);
    }

    private Vector2 GetOrthogonalInput(Vector2 rawInput) {
        // Если нет ввода, возвращаем нулевой вектор
        if (rawInput.magnitude == 0)
            return Vector2.zero;

        // Определяем доминирующее направление
        if (Mathf.Abs(rawInput.x) > Mathf.Abs(rawInput.y)) {
            // Движение влево/вправо
            return new Vector2(Mathf.Sign(rawInput.x), 0);
        }
        else {
            // Движение вверх/вниз
            return new Vector2(0, Mathf.Sign(rawInput.y));
        }
    }

}
