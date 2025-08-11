using UnityEngine;

public class NPCAttackHandler : MonoBehaviour, INPCComponent {

    [SerializeField] private string damageType;
    [SerializeField] private int damage = 1;
    [SerializeField] private float knockback = 7f;
    [SerializeField] private float hitStun = 1f;

    private NPC _npc;

    public void Init(NPC npc) {
        _npc = npc;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var playerHitbox = collision.GetComponent<IDamageable>();
        if (playerHitbox == null) return;

        Vector2 from = transform.position - collision.transform.position; // направление удара
        var info = new DamageInfo(damage, from, knockback, hitStun, damageType);

        playerHitbox.TakeDamage(info);
    }
}
