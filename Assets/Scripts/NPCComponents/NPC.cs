using UnityEngine;

public class NPC : StateMachine
{
    [SerializeField] private NPCType _type;

    [Header("Handlers")]
    [SerializeField] private NPCVisualHandler _visual;
    [SerializeField] private NPCMovementHandler _movement;
    [SerializeField] private NPCPatrolHandler _patrol;
    [SerializeField] private NPCPushReactionHandler _pushReaction;
    [SerializeField] private NPCThrowableReactionHandler _throwableReaction;
    [SerializeField] private NPCDropHandler _drop;
    [SerializeField] private NPCQuestSoupHandler _questSoup;

    // Public access for handlers
    public NPCVisualHandler Visual => _visual;
    public NPCMovementHandler Movement => _movement;
    public NPCPatrolHandler Patrol => _patrol;
    public NPCPushReactionHandler PushReaction => _pushReaction;
    public NPCThrowableReactionHandler ThrowableReaction => _throwableReaction;
    public NPCDropHandler Drop => _drop;
    public NPCQuestSoupHandler QuestSoup => _questSoup;
    public NPCType Type => _type;

    // States
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

    //----------------------------------------------------
    private void InitHandlers() {
        _visual?.Init(this);
        _movement?.Init(this);
        _patrol?.Init(this);
        _pushReaction?.Init(this);
        _throwableReaction?.Init(this);
        _drop?.Init(this);
        _questSoup?.Init(this);
    }

    private void InitStates() {
        _idleState = new NPCIdleState(this);
        _patrolState = new NPCPatrolState(this);
        _interactionState = new NPCInteractionState(this);
        _deadState = new NPCDeadState(this);
        _chaseState = new NPCChaseState(this);
        _attackState = new NPCAttackState(this);
    }
    //----------------------------------------------------
    private void Awake() {
        InitHandlers();
        InitStates();

        ChangeState(_type == NPCType.Enemy ? _patrolState : _idleState);
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }
}
