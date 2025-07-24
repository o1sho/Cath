using UnityEngine;

public abstract class NPCStateBase : IState {

    protected readonly NPC _npc;
    protected readonly NPCVisualHandler _visual;

    protected NPCStateBase(NPC npc) {
        _npc = npc;
        _visual = npc.Visual;
    }

    public abstract void Enter();
    public abstract void Update(float deltaTime);
    public abstract void Exit();
}
