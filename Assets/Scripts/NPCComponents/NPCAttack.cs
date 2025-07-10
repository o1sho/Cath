using UnityEngine;

public class NPCAttack : MonoBehaviour
{
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackCooldown = 3f;
    private float _cooldownTimer;

    public bool CanAttack => _cooldownTimer <= 0;
    public float AttackRange => attackRange;

    public void UpdateCooldown(float deltaTime) {
        _cooldownTimer -= deltaTime;
    }

    public void Attack(Player player) {
        if (_cooldownTimer > 0) return;
        if (Vector2.Distance(transform.position, player.transform.position) <= attackRange) {
            Debug.Log($"{gameObject.name} attacks player!");
            _cooldownTimer = attackCooldown;

            // Здесь можно добавить урон игроку
            //player.ChangeState(new PlayerDeadState(player));
        }
    }
}
