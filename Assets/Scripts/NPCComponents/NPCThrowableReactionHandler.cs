using Unity.VisualScripting;
using UnityEngine;

public class NPCThrowableReactionHandler : MonoBehaviour, INPCComponent, IThrowableTarget
{
    private NPC _npc;

    public void Init(NPC npc) {
        _npc = npc;
    }

    public void OnHitByItem(IThrowableItem item) {
        var reaction = item.GetReactionFor(_npc.Type);

        switch (reaction) {
            case ItemReactionType.Stun:
                //_npc.ChangeState(_npc.StunnedState);
                break;

            case ItemReactionType.MakeLight:
                _npc.PushReaction.SetLightWeight();
                Debug.Log($"{_npc.name} стал легким как перышко!");
                break;

            case ItemReactionType.OpenDialogue:
                _npc.ChangeState(_npc.InteractionState);
                break;

            case ItemReactionType.GiveItem:
                _npc.ChangeState(_npc.DeadState);
                Debug.Log($"{_npc.name} принял предмет: {item.Name}");
                break;

            case ItemReactionType.QuestTrigger:
                Debug.Log($"Квест активирован через {item.Name}");
                if (_npc.name == "SoupContainer") {
                    _npc.QuestSoup.IngredientHasBeenAdded(item);
                }
                break;

            case ItemReactionType.StartMoving:
                _npc.ChangeState(_npc.PatrolState);
                Debug.Log($"{_npc.name} проснулся и начал движение от попадания {item.Name}");
                break;

            case ItemReactionType.None:
            default:
                Debug.Log($"{_npc.name} не отреагировал на {item.Name}");
                break;
        }
    }
}
