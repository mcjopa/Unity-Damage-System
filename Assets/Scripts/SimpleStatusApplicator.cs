using Assets.Scripts;

public class SimpleStatusApplicator : IStatusApplicator
{
    public StatusEffect.StatusType StatusType { get; private set; }
    public float Amount { get; private set; }

    public SimpleStatusApplicator(StatusEffect.StatusType type, float amount)
    {
        Amount = amount;
        StatusType = type;
    }
}