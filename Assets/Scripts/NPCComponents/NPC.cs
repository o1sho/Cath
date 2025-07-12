using UnityEngine;
using UnityEngine.AI;
using static NPC;

public class NPC : StateMachine
{
    public enum NPCType {
        Enemy,
        Friendly
    }

    public enum NPCTypeOfMovement {
        Static,
        Dynamic
    }


    [SerializeField] private NPCType npcType;
    [SerializeField] private NPCTypeOfMovement npcTypeOfMovement;
    

    private NPCMovement _movement;
    private NPCVisual _visual;
    private NPCAttack _attack;


    public NPCMovement Movement => _movement;
    public NPCVisual Visual => _visual;
    public NPCAttack Attack => _attack;
    public NPCType NPCtype => npcType;
    public NPCTypeOfMovement NPCtypeOfMovement => npcTypeOfMovement;

    private void Awake() {
        _visual = GetComponent<NPCVisual>();
        _movement = GetComponent<NPCMovement>();
        _attack = GetComponent<NPCAttack>();

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
