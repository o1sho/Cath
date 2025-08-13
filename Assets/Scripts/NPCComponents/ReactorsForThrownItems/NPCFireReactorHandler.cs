using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class NPCFireReactorHandler : MonoBehaviour, INPCComponent, IThrownItemReactor
{
    private NPC _npc;
    public void Init(NPC npc) {
        _npc = npc;
    }
    private void Reset() => GetComponent<Collider2D>().isTrigger = true;
    public void OnThrownItemPassed(ThrownItemRuntime rt) {
        if (rt.Material == ItemMaterial.Wood || 
            rt.Material == ItemMaterial.Cloth) 
        {
            rt.Add(ThrownModifier.Fire);
        }
    }
}
