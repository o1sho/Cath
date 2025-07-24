using Unity.VisualScripting;
using UnityEngine;

public class NPCDeadState : NPCStateBase {
    public NPCDeadState(NPC npc) : base(npc) { }

    public override void Enter() {
        _npc.Movement.SetMovementMode(NPCMovementHandler.MovementMode.Frozen);
        _npc.Movement.Stop();

        _npc.Drop?.DropItems();

        //_npc.Visual.SetDead(true);

        _npc.DestroySelf();
      
    }

    public override void Update(float deltaTime) { }
    public override void Exit() { }
}
