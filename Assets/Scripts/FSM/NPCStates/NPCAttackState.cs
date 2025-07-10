using UnityEngine;

public class NPCAttackState : IState {
    private readonly NPC _npc;
    private readonly NPCVisual _visual;

    public NPCAttackState(NPC npc) {
        _npc = npc;
        _visual = _npc.Visual;
    }

    public void Enter() {

    }

    public void Update(float deltaTime) {

    }

    public void Exit() {
        
    }
}
