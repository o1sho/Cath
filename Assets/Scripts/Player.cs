using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private float maxMoveSpeed = 5f;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float deceleration = 30f;

    private Vector2 _inputVector;
    private Vector2 _velocity;
    private Rigidbody2D _rigidbody;

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
    }

    private void FixedUpdate() {
        HandleMovement();
    }

    private void HandleMovement() {
        Vector2 desiredVelocity = _inputVector * maxMoveSpeed;

        if (_inputVector.magnitude > 0) {
            _velocity = Vector2.MoveTowards(_velocity, desiredVelocity, acceleration * Time.fixedDeltaTime);
        }
        else {
            _velocity = Vector2.MoveTowards(_velocity, Vector2.zero, acceleration * Time.fixedDeltaTime);
        }

        _rigidbody.linearVelocity = _velocity;
    }

    public void Respawn() {
        transform.position = CurrentSpawnPoint;
    }

}
