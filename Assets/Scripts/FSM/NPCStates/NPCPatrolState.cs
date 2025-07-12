using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrolState : IState {
    private readonly NPC _npc;
    private readonly NPCVisual _visual;

    public NPCPatrolState(NPC npc) {
        _npc = npc;
        _visual = _npc.Visual;
    }

    public void Enter() {

    }

    public void Update(float deltaTime) {
        _npc.Movement.PatrolMove(deltaTime);
    }






    public void Exit() {

    }


}
