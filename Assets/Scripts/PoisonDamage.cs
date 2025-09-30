using UnityEngine;

public class PoisonDamage : ContinuousDamage
{
    public PoisonDamage(float damageAmount, float startDelay, float interval, float duration) :
        base(damageAmount, startDelay, interval, duration)
    { }

    public override void Tick(Health health)
    {
        health.Damage(m_damageAmount);
    }
}
