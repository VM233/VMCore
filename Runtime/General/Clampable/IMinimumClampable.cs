namespace VMFramework.Core
{
    public interface IMinimumClampable<in TValue>
    {
        public bool TryClampByMinimum(TValue minimum);
    }
}