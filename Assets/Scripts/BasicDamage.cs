using UnityEngine;

public class BasicDamage : Damage
{
    public BasicDamage(float damageAmount)
    {
        m_type = DAMAGE_TYPE.DISCRETE;
        m_damageAmount = damageAmount;
    }

    public override void Apply(Health health) 
    {
        health.Damage(m_damageAmount);
    }
}
