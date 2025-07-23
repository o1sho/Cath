using UnityEngine;

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

    private NPCIdleState _idleState;
    private NPCPatrolState _patrolState;
    private NPCInteractionState _interactionState;
    private NPCDeadState _deadState;
    private NPCChaseState _chaseState;
    private NPCAttackState _attackState;

    public IState IdleState => _idleState;
    public IState PatrolState => _patrolState;
    public IState InteractionState => _interactionState;
    public IState DeadState => _deadState;
    public IState ChaseState => _chaseState;
    public IState AttackState => _attackState;

    private void Awake() {
        _visual = GetComponent<NPCVisual>();
        _movementHandler ??= GetComponent<NPCMovementHandler>();
        _patrolHandler ??= GetComponent<NPCPatrolHandler>();
        _pushReactionHandler ??= GetComponent<NPCPushReactionHandler>();

        _idleState = new NPCIdleState(this);
        _patrolState = new NPCPatrolState(this);
        _interactionState = new NPCInteractionState(this);
        _deadState = new NPCDeadState(this);
        _chaseState = new NPCChaseState(this);
        _attackState = new NPCAttackState(this);

        ChangeState(npcType == NPCType.Enemy ? _patrolState : _idleState);
    }


}
