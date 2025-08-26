using UnityEngine;

public class NPCVisualHandler : MonoBehaviour, INPCComponent
{
    [SerializeField] private Sprite onHitSprite;

    private NPC _npc;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private SpriteAnimator _spriteAnimator;

    //---------------
    public void Init(NPC npc) {
        _npc = npc;
        if (_animator == null) _animator = GetComponent<Animator>();
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteAnimator == null) _spriteAnimator = GetComponent<SpriteAnimator>();
    }
    //---------------

    public void SetLocomotionState(bool isMoving, float blendX, float blendY) {
        _animator.SetBool("isMoving", isMoving);
        _animator.SetFloat("BlendX", blendX);
        _animator.SetFloat("BlendY", blendY);
    }

    public void SetFacingDirection(Vector2 direction) {
        //_animator.SetFloat("FacingX", direction.x);
        //_animator.SetFloat("FacingY", direction.y);
        if (_npc.Type == NPCType.Enemy) {
            if (direction.y > 0.1f) _spriteRenderer.flipX = false;
            else if (direction.y < -0.1f) _spriteRenderer.flipX = true;

            if (direction.x > 0.1f) _spriteRenderer.flipY = true;
            else if (direction.x < -0.1f) _spriteRenderer.flipY = false;
        }

        if (_npc.Type == NPCType.Platform) {
            if (direction.y > 0.1f) _spriteRenderer.flipY = false;
            else if (direction.y < -0.1f) _spriteRenderer.flipY = true;
        }
    }

    public void TriggerAttackAnimation() {
        _animator.SetTrigger("Attack");
    }

    public void TriggerTalkAnimation() {
        _animator.SetTrigger("Talk");
    }

    public void SetDeadState(bool isDead) {
        _animator.SetBool("isDead", isDead);
    }

    public void DisplayHandler(bool v) {
        _spriteRenderer.enabled = v;
    }

    public void SpriteAnimatorHandler(bool v) {
        if (_spriteAnimator != null) _spriteAnimator.enabled = v;
    }

    public void OnHitDisplay() {
        _spriteRenderer.sprite = onHitSprite;
    }
}
