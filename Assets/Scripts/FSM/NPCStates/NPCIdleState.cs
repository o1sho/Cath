using UnityEngine;

public class NPCIdleState : IState
{
    private readonly NPC _npc;
    private readonly NPCVisual _visual;

    public NPCIdleState(NPC npc) {
        _npc = npc;
        _visual = _npc.Visual;
    }

    public void Enter() {
        //_npc.Movement.Stop();
        if (_visual != null) {
            _visual.SetLocomotionState(isMoving: false, 0, 0);
        }
        Debug.Log($"{_npc.gameObject.name} entered Idle state");
    }

    public void Update(float deltaTime) {
        if (_npc.NPCtype == NPC.NPCType.Friendly) {
            // ѕроверка взаимодействи€ дл€ интерактивных NPC
            //if (GameInput.Instance != null && GameInput.Instance.InputSystem != null &&
            //    GameInput.Instance.InputSystem.Player.Interact.WasPressedThisFrame()) {
            //    Collider2D[] colliders = Physics2D.OverlapCircleAll(_npc.transform.position, 1f);
            //    foreach (var collider in colliders) {
            //        if (collider.CompareTag("Player")) {
            //            _npc.ChangeState(new NPCTalkState(_npc));
            //            return;
            //        }
            //    }
            //}
        }
    }

    public void Exit() { }

}
