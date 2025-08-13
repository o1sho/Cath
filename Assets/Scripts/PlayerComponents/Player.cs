using UnityEngine;

public class Player : StateMachine
{
    public static Player Instance { get; private set; }

    [Header("Player Settings")]
    [SerializeField] private Transform throwSpawnPoint;
    [SerializeField] private Vector3 currentSpawnPoint;

    [Header("Handlers")]
    [SerializeField] private PlayerMovementHandler _movement;
    [SerializeField] private PlayerDashHandler _dash;
    [SerializeField] private PlayerThrowHandler _throw;
    [SerializeField] private PlayerGroundCheckHandler _groundCheck;
    [SerializeField] private PlayerVisualHandler _visual;
    [SerializeField] private PlayerHealthHandler _health;
    [SerializeField] private PlayerHeldItemDisplayHandler _heldItemDisplay;

    private Rigidbody2D _rigidbody;

    public Vector3 CurrentSpawnPoint { get => currentSpawnPoint; set => currentSpawnPoint = value; }

    // Public access for handlers
    public PlayerMovementHandler Movement => _movement;
    public PlayerDashHandler Dash => _dash;
    public PlayerThrowHandler Throw => _throw;
    public PlayerGroundCheckHandler GroundCheck => _groundCheck;
    public PlayerVisualHandler Visual => _visual;
    public PlayerHealthHandler Health => _health;
    public PlayerHeldItemDisplayHandler HeldItemDisplay => _heldItemDisplay;
    public Rigidbody2D Rigidbody => _rigidbody;

    // States
    private PlayerIdleState _idleState;
    private PlayerMovingState _movingState;
    private PlayerDashingState _dashingState;
    private PlayerFallState _fallState;
    private PlayerThrowingState _throwingState;
    private PlayerPickupState _pickupState;
    private PlayerHurtState _hurtState;
    private PlayerDeadState _deadState;
    public IState IdleState => _idleState;
    public IState MovingState => _movingState;
    public IState DashingState => _dashingState;
    public IState FallState => _fallState;
    public IState ThrowingState => _throwingState;
    public IState PickupState => _pickupState;
    public IState HurtState => _hurtState;
    public IState DeadState => _deadState;

    //----------------------------------------------------
    private void InitHandlers() {
        _visual?.Init(this);
        _movement?.Init(this);
        _dash?.Init(this);
        _throw?.Init(this);
        _groundCheck?.Init(this);
        _health?.Init(this);
        _heldItemDisplay?.Init(this);
    }

    private void InitStates() {
        _idleState = new PlayerIdleState(this);
        _movingState = new PlayerMovingState(this);
        _dashingState = new PlayerDashingState(this);
        _fallState = new PlayerFallState(this);
        _throwingState = new PlayerThrowingState(this);
        _pickupState = new PlayerPickupState(this);
        _hurtState = new PlayerHurtState(this);
        _deadState = new PlayerDeadState(this);
    }
    //----------------------------------------------------

    private void Awake() {
        Instance = this;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0f;
        _rigidbody.freezeRotation = true;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        InitHandlers();  // —борка модулей
        InitStates();    // —борка состо€ний

        ChangeState(_idleState);
    }

    private void Start() {
        _movement?.ResetVelocity();
    }

    private void FixedUpdate() {
        _rigidbody.linearVelocity = _movement.Velocity;
    }

    public void Respawn() {
        transform.position = currentSpawnPoint;

        _visual?.ResetAllAnimationStates();
        _movement?.ResetVelocity();
        _dash?.ResetCooldown();
        _throw?.ClearHeldItem();
        ChangeState(_idleState);
    }
}
