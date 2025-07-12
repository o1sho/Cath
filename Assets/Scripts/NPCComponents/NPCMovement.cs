using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Rigidbody2D _rigidbody;
    private Vector2 _inputVector;
    private Vector2 _lastNonZeroInput = Vector2.right;
    private Vector2 _velocity;

    private int currentPointIndex = 0;
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] private float minDistance = 0.1f;

    public Vector2 InputVector => _inputVector;
    public Vector2 LastNonZeroInput => _lastNonZeroInput;
    public Vector2 Velocity => _velocity;

    public Transform[] PatrolPoints => patrolPoints;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0f;
        _rigidbody.freezeRotation = true;
    }

    private void FixedUpdate() {
        //_rigidbody.linearVelocity = _velocity;
    }

    public void UpdateInput(Vector2 input) {
        _inputVector = input;
        if (_inputVector.magnitude > 0) {
            _lastNonZeroInput = _inputVector.normalized;
        }
        Debug.Log($"NPCMovement Input: {_inputVector}");
    }

    public void Move(float deltaTime) {
        _velocity = _inputVector.normalized * moveSpeed;
        Debug.Log($"NPCMovement Velocity: {_velocity}");
    }

    public void PatrolMove(float deltaTime) {
        if (patrolPoints.Length == 0) return;
        Vector2 targetPosition = patrolPoints[currentPointIndex].position;
        Vector2 moveDirection = (targetPosition - _rigidbody.position).normalized;

        _rigidbody.linearVelocity = moveDirection * moveSpeed;

        if (Vector2.Distance(_rigidbody.position, targetPosition) < minDistance) {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    public void Stop() {
        _velocity = Vector2.zero;
        _inputVector = Vector2.zero;
        _rigidbody.linearVelocity = Vector2.zero;
    }

    public void ResetVelocity() {
        _velocity = Vector2.zero;
        _rigidbody.linearVelocity = Vector2.zero;
    }
}
