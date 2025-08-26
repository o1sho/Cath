using Unity.VisualScripting;
using UnityEngine;

public class NPCThrowableReactionHandler : MonoBehaviour, INPCComponent, IThrownItemConsumer
{
    private NPC _npc;
    private Collider2D _collider;

    public void Init(NPC npc) {
        _npc = npc;
        if (_collider == null) _collider = GetComponent<Collider2D>();
    }

    public HitOutcome OnHitBy(ThrownItemRuntime rt) {
        if (rt == null) return HitOutcome.DestroyProjectile;

        if (_npc.Type == NPCType.Inanimate && _npc.name.Contains("Bush") && rt.Has(ThrownModifier.Fire)) {
            _npc.ChangeState(_npc.DeadState);
            return HitOutcome.DestroyProjectile;
        }

        //вызываетс€ если нет никаких кастомных реакторов. –еакци€ беретс€ из свойств лет€щего предмета
        var reaction = rt.SourceItem?.GetReactionFor(_npc.Type);
        switch (reaction) {
            case ItemReactionType.Stun:
                //_npc.ChangeState(_npc.StunnedState);
                return HitOutcome.DestroyProjectile;

            case ItemReactionType.MakeLight:
                _npc.PushReaction.SetLightWeight();
                Debug.Log($"{_npc.name} стал легким как перышко!");
                return HitOutcome.DestroyProjectile;

            case ItemReactionType.OpenDialogue:
                _npc.ChangeState(_npc.InteractionState);
                return HitOutcome.DestroyProjectile;

            case ItemReactionType.GiveItem:
                _npc.ChangeState(_npc.DeadState);
                Debug.Log($"{_npc.name} прин€л предмет: {rt.SourceItem.Name}");
                return HitOutcome.DestroyProjectile;

            case ItemReactionType.QuestTrigger:
                Debug.Log($" вест активирован через {rt.SourceItem.Name}");
                if (_npc.name == "SoupContainer") {
                    _npc.QuestSoup.IngredientHasBeenAdded(rt.SourceItem);
                }
                return HitOutcome.DestroyProjectile;

            case ItemReactionType.StartMoving:
                _npc.ChangeState(_npc.PatrolState);
                Debug.Log($"{_npc.name} проснулс€ и начал движение от попадани€ {rt.SourceItem.Name}");
                return HitOutcome.DestroyProjectile;

            case ItemReactionType.Hide:
                //_npc.ChangeState(_npc.PatrolState);
                Debug.Log($"{_npc.name} пр€четс€ от попадани€ {rt.SourceItem.Name}");
                _npc.Visual.SpriteAnimatorHandler(false);
                _npc.Visual.OnHitDisplay();
                _collider.enabled = false;
                return HitOutcome.ContinueFlight;

            case ItemReactionType.None:
            default:
                Debug.Log($"{_npc.name} не отреагировал на {rt.SourceItem.Name}");
                return HitOutcome.DestroyProjectile;
        }
    }
}
