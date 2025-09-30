using UnityEngine;

public class FireDamage : ContinuousDamage
{
    private float m_damageScaling = 1.2f;

    public FireDamage(float damageAmount, float startDelay, float interval, float duration) :
        base(damageAmount, startDelay, interval, duration)
    { }

    public override void Tick(Health health)
    {
        health.Damage(m_damageAmount);
        m_damageAmount *= m_damageScaling;
    }
}
