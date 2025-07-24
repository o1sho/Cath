using UnityEngine;

public class NPCMovementHandler : MonoBehaviour, INPCComponent
{
    private NPC _npc;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    private Vector2 _velocity;

    public enum MovementMode {
        Input,     // Управляется ИИ
        Override,  // Внешняя сила (удар, отталкивание)
        Frozen     // Полная остановка
    }

    private MovementMode _currentMode = MovementMode.Input;
    private Vector2 _overrideVelocity;

    //---------------
    public void Init(NPC npc) {
        _npc = npc;
        if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0f;
        _rigidbody.freezeRotation = true;
    }
    //---------------

    public void Stop() {
        _velocity = Vector2.zero;
        _direction = Vector2.zero;
        _rigidbody.linearVelocity = Vector2.zero;
    }

    public void ResetVelocity() {
        _velocity = Vector2.zero;
        _rigidbody.linearVelocity = Vector2.zero;
    }

    public void SetMovementMode(MovementMode mode) {
        if (_currentMode != mode) {
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

    private void FixedUpdate() {
        Vector2 finalVelocity = Vector2.zero;

        switch (_currentMode) {
            case MovementMode.Input:
                finalVelocity = _direction * moveSpeed;
                break;

            case MovementMode.Override:
                finalVelocity = _overrideVelocity;
                break;

            case MovementMode.Frozen:
                finalVelocity = Vector2.zero;
                break;
        }

        _velocity = finalVelocity;
        _rigidbody.linearVelocity = finalVelocity;
    }
}
