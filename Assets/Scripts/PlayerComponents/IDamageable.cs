public interface IDamageable
{
    bool TakeDamage(DamageInfo info);
    bool IsInvulnerable { get; }
}
