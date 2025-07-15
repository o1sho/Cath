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
            //if (_npc.InteractionCheck.IsPlayer) {
            //    _npc.ChangeState(new NPCInteractionState(_npc));
            //}
        }
    }

    public void Exit() { }

}
