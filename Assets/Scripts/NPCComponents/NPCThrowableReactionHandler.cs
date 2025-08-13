using Unity.VisualScripting;
using UnityEngine;

public class NPCThrowableReactionHandler : MonoBehaviour, INPCComponent, IThrownItemConsumer
{
    private NPC _npc;

    public void Init(NPC npc) {
        _npc = npc;
    }

    public void OnHitBy(ThrownItemRuntime rt) {
        if (rt == null) return;

        if (_npc.Type == NPCType.Inanimate && _npc.name.Contains("Bush") && rt.Has(ThrownModifier.Fire)) {
            _npc.ChangeState(_npc.DeadState);
            return;
        }

        //вызывается если нет никаких кастомных реакторов. Реакция берется из свойств летящего предмета
        var reaction = rt.SourceItem?.GetReactionFor(_npc.Type);
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
                Debug.Log($"{_npc.name} принял предмет: {rt.SourceItem.Name}");
                break;

            case ItemReactionType.QuestTrigger:
                Debug.Log($"Квест активирован через {rt.SourceItem.Name}");
                if (_npc.name == "SoupContainer") {
                    _npc.QuestSoup.IngredientHasBeenAdded(rt.SourceItem);
                }
                break;

            case ItemReactionType.StartMoving:
                _npc.ChangeState(_npc.PatrolState);
                Debug.Log($"{_npc.name} проснулся и начал движение от попадания {rt.SourceItem.Name}");
                break;

            case ItemReactionType.None:
            default:
                Debug.Log($"{_npc.name} не отреагировал на {rt.SourceItem.Name}");
                break;
        }
    }
}
