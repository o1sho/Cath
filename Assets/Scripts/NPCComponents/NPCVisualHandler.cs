using UnityEngine;

public class NPCVisualHandler : MonoBehaviour, INPCComponent
{
    private NPC _npc;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    //---------------
    public void Init(NPC npc) {
        _npc = npc;
        if (_animator == null) _animator = GetComponent<Animator>();
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
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

        if (direction.y > 0.1f) _spriteRenderer.flipX = false;
        else if (direction.y < -0.1f) _spriteRenderer.flipX = true;

        if (direction.x > 0.1f) _spriteRenderer.flipY = true;
        else if (direction.x < -0.1f) _spriteRenderer.flipY = false;
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
}
