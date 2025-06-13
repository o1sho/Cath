using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private float moveSpeed = 10f;

    private Vector2 _inputVector;
    private Rigidbody2D _rigidbody;
    private float _minMoveSpeed = 0.1f;

    [SerializeField] private bool _isIdle = false;
    [SerializeField] private bool _isMovingSide = false;
    [SerializeField] private bool _isMovingBack = false;
    [SerializeField] private bool _isMovingFront = false;


    private void Awake() {
        Instance = this;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        _inputVector = GameInput.Instance.GetMovementVector();
    }

    private void FixedUpdate() {
        HandleMovement();
    }

    private void HandleMovement() {
        _rigidbody.MovePosition(_rigidbody.position + _inputVector * (moveSpeed * Time.fixedDeltaTime));


        if (Mathf.Abs(_inputVector.x) > _minMoveSpeed || Mathf.Abs(_inputVector.y) > _minMoveSpeed) {
            _isIdle = false;
        }
        else {
            _isIdle = true;
        }

        if (_inputVector.y > 0) {
            _isMovingBack = true;
            _isMovingFront = false;
        }
        if (_inputVector.y < 0) {
            _isMovingBack = false;
            _isMovingFront = true;
        }

        if (Mathf.Abs(_inputVector.x) > _minMoveSpeed && _inputVector.y == 0) {
            _isMovingSide = true;
        }
        if (Mathf.Abs(_inputVector.x) == 0 && _inputVector.y == 0) {
            _isMovingSide = false;
            _isMovingBack = false;
            _isMovingFront = false;
        }
    }

    public bool IsIdle() { return _isIdle; }
    public bool IsMovingSide() {return _isMovingSide; }
    public bool IsMovingBack() { return _isMovingBack; }
    public bool IsMovingFront() { return _isMovingFront; }
}
