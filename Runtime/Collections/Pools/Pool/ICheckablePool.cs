namespace VMFramework.Core.Pools
{
    public interface ICheckablePool<in T> : IPool<T>
    {
        public bool Contains(T item);
    }
}