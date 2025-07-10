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

    private NPCMovement _movement;
    private NPCVisual _visual;
    private NPCAttack _attack;


    public NPCMovement Movement => _movement;
    public NPCVisual Visual => _visual;
    public NPCAttack Attack => _attack;
    public NPCType NPCtype => npcType;

    private void Awake() {
        _visual = GetComponent<NPCVisual>();
        _movement = GetComponent<NPCMovement>();
        _attack = GetComponent<NPCAttack>();

        switch (npcType) {
            case NPCType.Enemy:
                ChangeState(new NPCIdleState(this));
                break;
            case NPCType.Friendly:
                ChangeState(new NPCIdleState(this));
                break;
        }  
    }
}
