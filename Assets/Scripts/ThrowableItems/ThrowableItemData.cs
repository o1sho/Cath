using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemForThrowing", menuName = "ThrowableItems/ThrowableItemData")]
public class ThrowableItemData : ScriptableObject, IThrowableItem {

    [SerializeField] private string _itemName = "Item";
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _throwSpeed = 10f;
    [SerializeField] private float angularSpeed = 360f;
    [SerializeField] private Sprite displaySprite;

    [Header("NPC Reactions")]
    [SerializeField] private List<NPCReactionEntry> reactions;

    public string Name => _itemName;
    public GameObject Prefab => _prefab;
    public float ThrowSpeed => _throwSpeed;
    public float AngularSpeed => angularSpeed;
    public Sprite DisplaySprite => displaySprite;

    public void OnThrow(Vector2 direction) {
        Debug.Log($"Throwing {_itemName} in direction {direction}");
    }

    public void OnPickup(Player player) {
        Debug.Log($"{Name} picked up by {player.name}");
        // можно добавить звук, событие, эффект
    }

    public ItemReactionType GetReactionFor(NPCType npcType) {
        foreach (var entry in reactions) {
            if (entry.npcType == npcType) return entry.reaction;
        }

        return ItemReactionType.None;
    }
}
