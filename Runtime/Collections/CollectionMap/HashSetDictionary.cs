using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Core.Pools;

namespace VMFramework.Core
{
    public class HashSetDictionary<TKey, TValue> : ICollectionMap<TKey, TValue>
    {
        public IReadOnlyCollection<TKey> Keys => dictionary.Keys;

        public event IReadOnlyKeysOwner<TKey>.KeyAddedHandler OnKeyAdded;
        public event IReadOnlyKeysOwner<TKey>.KeyRemovedHandler OnKeyRemoved;

        [ShowInInspector]
        protected readonly Dictionary<TKey, HashSet<TValue>> dictionary = new();

        public void Clear()
        {
            foreach (var (key, collection) in dictionary)
            {
                OnKeyRemoved?.Invoke(key);
                collection.ReturnToSharedPool();
            }
            
            dictionary.Clear();
        }

        public bool Add(TKey key, TValue value, out bool isNewKey)
        {
            if (dictionary.TryGetValue(key, out var collection) == false)
            {
                collection = HashSetPool<TValue>.Shared.Get();
                collection.Clear();
                dictionary.Add(key, collection);
                OnKeyAdded?.Invoke(key);
                isNewKey = true;
                return true;
            }

            var oldCount = collection.Count;
            collection.Add(value);
            isNewKey = false;
            return oldCount != collection.Count;
        }

        public bool Remove(TKey key, TValue value, out bool keyRemoved)
        {
            if (dictionary.TryGetValue(key, out var collection) == false)
            {
                keyRemoved = false;
                return false;
            }

            if (collection.Remove(value) == false)
            {
                keyRemoved = false;
                return false;
            }

            if (collection.Count == 0)
            {
                dictionary.Remove(key);
                collection.ReturnToSharedPool();
                OnKeyRemoved?.Invoke(key);
                keyRemoved = true;
            }
            else
            {
                keyRemoved = false;
            }
            
            return true;
        }
    }
}