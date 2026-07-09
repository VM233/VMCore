namespace VMFramework.Core
{
    public interface IMaximumClampable<in TValue>
    {
        public bool TryClampByMaximum(TValue maximum);
    }
}