namespace VMFramework.Core
{
    public interface ICollectionMap<TKey, in TValue> : IReadOnlyKeysOwner<TKey>
    {
        public bool Add(TKey key, TValue value, out bool isNewKey);

        public bool Remove(TKey key, TValue value, out bool keyRemoved);
    }
}