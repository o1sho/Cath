using UnityEngine;

public class NPCInteractionState : NPCStateBase {
    public NPCInteractionState(NPC npc) : base(npc) { }

    public override void Enter() {
        Debug.Log("123123");
    }

    public override void Update(float deltaTime) {

    }

    public override void Exit() { }
}
