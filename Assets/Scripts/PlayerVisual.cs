using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private const string IS_MOVING_SIDE = "isMovingSide";
    private const string IS_MOVING_BACK = "isMovingBack";
    private const string IS_MOVING_FRONT = "isMovingFront";
    private const string IS_IDLE = "isIdle";

    private void Awake() {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (Player.Instance.IsIdle()) _animator.SetTrigger(IS_IDLE);
        if (Player.Instance.IsMovingSide()) _animator.SetTrigger(IS_MOVING_SIDE);
        if (Player.Instance.IsMovingBack()) _animator.SetTrigger(IS_MOVING_BACK);
        if (Player.Instance.IsMovingFront()) _animator.SetTrigger(IS_MOVING_FRONT);
    }

    private void HandlePlayerFacingDirection() {

    }
}
