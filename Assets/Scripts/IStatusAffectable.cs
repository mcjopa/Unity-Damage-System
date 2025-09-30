namespace Assets.Scripts
{
  public interface IStatusEffectable
  {
    void ApplyStatus(IStatusApplicator status);
  }
}

// I want a ditionary of each status that I can apply 