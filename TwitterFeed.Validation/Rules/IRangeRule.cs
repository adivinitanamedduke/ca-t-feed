namespace CCS.Validation
{
    public interface IRangeRule : IRule
    {
        object GetMinimum();

        object GetMaximum();
    }
}