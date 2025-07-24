using System.Collections.Generic;
using UnityEngine;

public class NPCDropHandler : MonoBehaviour, INPCComponent
{
    [SerializeField] private List<ThrowableItemData> dropItems;
    [SerializeField] private GameObject dropPrefab;

    private NPC _npc;

    public void Init(NPC npc) {
        _npc = npc;
    }

    public void DropItems() {
        if (dropPrefab == null) {
            Debug.LogWarning($"[{_npc.name}] DropHandler: dropPrefab не назначен");
            return;
        }

        foreach (var item in dropItems) {
            if (item == null) continue;

            var dropped = Instantiate(dropPrefab, _npc.transform.position, Quaternion.identity);

            if (dropped.TryGetComponent<ItemPickup>(out var pickup)) {
                pickup.SetItem(item);
            }
        }
    }
}
