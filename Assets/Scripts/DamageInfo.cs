using UnityEngine;

public class DamageInfo
{
    public int Amount;
    public Vector2 HitFrom;
    public float KnockbackForce;
    public float HitStunDuration;
    public string SourceTag; //тип удара/врага

    public DamageInfo(int amount, Vector2 hitFrom, float knockbackForce, float hitStunDuration, string sourceTag) {
        Amount = amount;
        HitFrom = hitFrom.normalized;
        KnockbackForce = knockbackForce;
        HitStunDuration = hitStunDuration;
        SourceTag = sourceTag;
    }
}
