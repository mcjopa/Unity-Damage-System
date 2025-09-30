namespace Assets.Scripts
{
    public interface IStatusApplicator
    {
        StatusEffect.StatusType StatusType { get; }
        float Amount { get; }
    }
}

