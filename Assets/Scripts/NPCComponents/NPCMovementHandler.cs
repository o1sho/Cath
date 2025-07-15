using UnityEngine;

public class NPCMovementHandler : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Rigidbody2D _rigidbody;
    private Vector2 _inputVector;
    private Vector2 _lastNonZeroInput = Vector2.right;
    private Vector2 _velocity;



    public Vector2 InputVector => _inputVector;
    public Vector2 LastNonZeroInput => _lastNonZeroInput;
    public Vector2 Velocity => _velocity;

    

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0f;
        _rigidbody.freezeRotation = true;
    }

    public void UpdateInput(Vector2 input) {
        _inputVector = input;
        if (_inputVector.magnitude > 0) {
            _lastNonZeroInput = _inputVector.normalized;
        }
        Debug.Log($"NPCMovement Input: {_inputVector}");
    }

    public void Move(float deltaTime) {
        _velocity = _inputVector.normalized * moveSpeed * deltaTime;
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
