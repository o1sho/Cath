using UnityEngine;

public class NPCInteractionState : IState {
    private readonly NPC _npc;
    private readonly NPCVisual _visual;

    public NPCInteractionState(NPC npc) {
        _npc = npc;
        _visual = _npc.Visual;
    }

    public void Enter() {
        Debug.Log("123123");
    }

    public void Update(float deltaTime) {

    }

    public void Exit() { }
}
