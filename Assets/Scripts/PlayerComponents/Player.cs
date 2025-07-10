using UnityEngine;

public class Player : StateMachine
{
    public static Player Instance { get; private set; }

    [Header("Player Settings")]
    [SerializeField] private Transform throwSpawnPoint; // Точка спавна предмета
    [SerializeField] private Vector3 currentSpawnPoint;
    [SerializeField] private PlayerGroundCheck groundCheck;

    private Rigidbody2D _rigidbody;
    private PlayerMovement _movement;
    private PlayerDash _dash;
    private PlayerThrow _throw;

    [HideInInspector] public Rigidbody2D Rigidbody => _rigidbody;
    public Vector3 CurrentSpawnPoint { get => currentSpawnPoint; set => currentSpawnPoint = value; }
    public PlayerMovement Movement => _movement;
    public PlayerDash Dash => _dash;
    public PlayerThrow Throw => _throw;
    public PlayerGroundCheck GroundCheck => groundCheck;

    private void Awake() {
        Instance = this;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0f;
        _rigidbody.freezeRotation = true;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        _movement = GetComponent<PlayerMovement>();
        _dash = GetComponent<PlayerDash>();
        _throw = GetComponent<PlayerThrow>();

        // Начальное состояние
        ChangeState(new PlayerIdleState(this));
    }


    private void FixedUpdate() {
        _rigidbody.linearVelocity = _movement.Velocity; // Применяем скорость от PlayerMovement
    }

    public void Respawn() {
        transform.position = currentSpawnPoint;
        _movement.ResetVelocity();
        _dash.ResetCooldown();
        _throw.ClearHeldItem();
        ChangeState(new PlayerIdleState(this));
    }

    //private void OnTriggerStay2D(Collider2D collision) {
    //    if (collision.CompareTag("FallArea")) {
    //        ChangeState(new PlayerFallState(this));
    //    }
    //}
}
