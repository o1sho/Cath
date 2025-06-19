using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private const string IS_IDLE = "isIdle";
    private const string IS_MOVING = "isMoving";
    private const string IS_DASHING = "isDashing";
    private const string IS_DEAD = "isDead";
    private const string IS_DASH_BACK = "isDashBack";
    private const string IS_DASH_FRONT = "isDashFront";
    private const string IS_DASH_SIDE = "isDashSide";
    private const string VELOCITY_X = "velocityX";
    private const string VELOCITY_Y = "velocityY";

    private void Awake() {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        var stateMachine = Player.Instance.GetComponent<StateMachine>();
        var currentState = stateMachine.CurrentState;

        _animator.SetBool(IS_IDLE, currentState is PlayerIdleState);
        _animator.SetBool(IS_MOVING, currentState is PlayerMovingState);
        _animator.SetBool(IS_DASHING, currentState is PlayerDashingState);
        _animator.SetBool(IS_DEAD, currentState is PlayerDeadState);

        _animator.SetFloat(VELOCITY_X, Mathf.Abs(Player.Instance.Rigidbody.linearVelocity.x));
        _animator.SetFloat(VELOCITY_Y, Player.Instance.Rigidbody.linearVelocity.y);

        HandlePlayerFacingDirection();
    }

    public void TriggerDashAnimation(string trigger) {
        _animator.SetTrigger(trigger);
    }

    private void HandlePlayerFacingDirection() {
        if (Player.Instance.InputVector.x > 0) {
            _spriteRenderer.flipX = true; // Вправо
        }
        else if (Player.Instance.InputVector.x < 0) {
            _spriteRenderer.flipX = false; // Влево
        }
    }
}
