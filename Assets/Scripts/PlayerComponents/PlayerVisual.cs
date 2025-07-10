using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private const string IS_MOVING = "isMoving";
    private const string IS_DEAD = "isDead";
    private const string IS_THROWING = "isThrowing";
    private const string IS_DASH_BACK = "isDashBack";
    private const string IS_DASH_FRONT = "isDashFront";
    private const string IS_DASH_SIDE = "isDashSide";
    private const string IS_PICKUP = "isPickup";
    private const string VELOCITY_X = "velocityX";
    private const string VELOCITY_Y = "velocityY";
    private const string IS_FALL = "isFall";
    

    private void Awake() {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetLocomotionState(bool isMoving, float velocityX, float velocityY) {
        _animator.SetBool(IS_MOVING, isMoving);
        _animator.SetFloat(VELOCITY_X, velocityX);
        _animator.SetFloat(VELOCITY_Y, velocityY);
    }

    public void SetDeadState(bool isDead) {
        _animator.SetTrigger(IS_DEAD);
    }

    public void TriggerDashAnimation(string trigger) {
        _animator.SetTrigger(trigger);
    }

    public void TriggerThrowAnimation(string trigger) {
        _animator.SetTrigger(trigger);
    }

    public void SetPickupAnimation(bool isPickup) {
        _animator.SetBool(IS_PICKUP, isPickup);
    }

    public void TriggerAnimation(string trigger) {
        _animator.SetTrigger(trigger);
    }

    public void SetFacingDirection(Vector2 direction) {
        if (direction.x > 0) {
            _spriteRenderer.flipX = true; // Вправо
        }
        else if (direction.x < 0) {
            _spriteRenderer.flipX = false; // Влево
        }
    }
}
