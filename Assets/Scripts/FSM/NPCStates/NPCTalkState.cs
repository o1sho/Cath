using UnityEngine;

public class NPCTalkState : IState {
    private readonly NPC _npc;
    private readonly NPCVisual _visual;

    public NPCTalkState(NPC npc) {
        _npc = npc;
        _visual = _npc.Visual;
    }

    public void Enter() {

    }

    public void Update(float deltaTime) {

    }

    public void Exit() { }
}
