using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private const string IS_IDLE = "isIdle";
    private const string IS_VELOCITY_X_ZERO = "isVelocityXZero";
    private const string IS_VELOCITY_Y_ZERO = "isVelocityYZero";

    private bool _isVelocityXZero;
    private bool _isVelocityYZero;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (Player.Instance.Rigidbody.linearVelocity.magnitude == 0) _animator.SetTrigger(IS_IDLE);

        if (Player.Instance.Rigidbody.linearVelocity.x == 0) {
            _isVelocityXZero = true;
        }
        else {
            _isVelocityXZero = false;
        }

        if (Player.Instance.Rigidbody.linearVelocity.y == 0) {
            _isVelocityYZero = true;
        }
        else {
            _isVelocityYZero = false;
        }

        _animator.SetFloat("velocityX", Mathf.Abs(Player.Instance.Rigidbody.linearVelocity.x));
        _animator.SetFloat("velocityY", Player.Instance.Rigidbody.linearVelocity.y);
        _animator.SetBool(IS_VELOCITY_X_ZERO, _isVelocityXZero);
        _animator.SetBool(IS_VELOCITY_Y_ZERO, _isVelocityYZero);


        HandlePlayerFacingDirection();
    }

    private void HandlePlayerFacingDirection() {
        if (Player.Instance.Rigidbody.linearVelocity.x > 0) {
            _spriteRenderer.flipX = true;
        }
        else {
            _spriteRenderer.flipX = false;
        }
    }
}
