using UnityEngine;
using UnityEngine.AI;
using static NPC;

public class NPC : StateMachine
{
    public enum NPCType {
        Enemy,
        Friendly
    }


    [SerializeField] private NPCType npcType;

    private NPCVisual _visual;
    private NPCMovementHandler _movementHandler;
    private NPCPatrolHandler _patrolHandler;
    private NPCPushReactionHandler _pushReactionHandler;



    public NPCVisual Visual => _visual;
    public NPCMovementHandler Movement => _movementHandler;
    public NPCPatrolHandler PatrolHandler => _patrolHandler;
    public NPCPushReactionHandler PushReactionHandler => _pushReactionHandler;
    public NPCType NPCtype => npcType;

    private void Awake() {
        _visual = GetComponent<NPCVisual>();
        _movementHandler ??= GetComponent<NPCMovementHandler>();
        _patrolHandler ??= GetComponent<NPCPatrolHandler>();
        _pushReactionHandler ??= GetComponent<NPCPushReactionHandler>();

        switch (npcType) {
            case NPCType.Enemy:
                ChangeState(new NPCPatrolState(this));
                break;
            case NPCType.Friendly:
                ChangeState(new NPCIdleState(this));
                break;
        }  
    }


}
