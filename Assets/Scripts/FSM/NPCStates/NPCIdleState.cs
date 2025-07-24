using UnityEngine;

public class NPCIdleState : NPCStateBase
{
    public NPCIdleState(NPC npc) : base(npc) { }


    public override void Enter() {
        //_visual?.SetLocomotionState(false, 0, 0);
        Debug.Log($"{_npc.name} entered Idle state");
    }

    public override void Update(float deltaTime) {

    }

    public override void Exit() { }

}
