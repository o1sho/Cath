using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrolState : NPCStateBase {
    public NPCPatrolState(NPC npc) : base(npc) { }

    public override void Enter() {

    }

    public override void Update(float deltaTime) {
        _npc.Patrol.UpdatePatrol(deltaTime);
    }

    public override void Exit() {

    }
}
