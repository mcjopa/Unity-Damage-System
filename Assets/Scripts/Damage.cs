public abstract class Damage
{
    public enum DAMAGE_TYPE
    {
        DISCRETE,
        CONTINUOUS
    }

    protected DAMAGE_TYPE m_type;
    public DAMAGE_TYPE Type { get { return m_type; } }

    protected float m_damageAmount;

    public abstract void Apply(Health health);
}

public class DiscreteDamage : Damage
{
    public DiscreteDamage(float damageAmount)
    {
        m_type = DAMAGE_TYPE.DISCRETE;
        m_damageAmount = damageAmount;
    }

    public override void Apply(Health health)
    {
        health.Damage(m_damageAmount);
    }
}

public abstract class ContinuousDamage : Damage
{
    protected float m_startDelay;
    public float StartDelay { get { return m_startDelay; } }

    protected float m_interval;
    public float Interval { get { return m_interval; } }

    protected float m_duration;
    public float Duration { get { return m_duration; } }


    public override void Apply(Health health)
    {

    }

    public ContinuousDamage(float damageAmount, float startDelay, float interval, float duration)
    {
        m_damageAmount = damageAmount;
        m_startDelay = startDelay;
        m_interval = interval;
        m_duration = duration;
    }

    public abstract void Tick(Health health);
}