using UnityEngine;

public class NPCIdleState : NPCStateBase
{
    public NPCIdleState(NPC npc) : base(npc) { }


    public override void Enter() {
        _visual?.SetLocomotionState(false, 0, 0);
        Debug.Log($"{_npc.name} entered Idle state");
    }

    public override void Update(float deltaTime) {
        if (_npc.NPCtype == NPC.NPCType.Friendly) {
            //if (_npc.InteractionCheck.IsPlayer) {
            //    _npc.ChangeState(new NPCInteractionState(_npc));
            //}
        }
    }

    public override void Exit() { }

}
