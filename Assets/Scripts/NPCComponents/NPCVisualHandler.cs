using UnityEngine;

public class NPCVisualHandler : MonoBehaviour, INPCComponent
{
    private NPC _npc;
    private Animator _animator;

    //---------------
    public void Init(NPC npc) {
        _npc = npc;
        if (_animator == null) _animator = GetComponent<Animator>();
    }
    //---------------

    public void SetLocomotionState(bool isMoving, float blendX, float blendY) {
        _animator.SetBool("isMoving", isMoving);
        _animator.SetFloat("BlendX", blendX);
        _animator.SetFloat("BlendY", blendY);
    }

    public void SetFacingDirection(Vector2 direction) {
        _animator.SetFloat("FacingX", direction.x);
        _animator.SetFloat("FacingY", direction.y);
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
