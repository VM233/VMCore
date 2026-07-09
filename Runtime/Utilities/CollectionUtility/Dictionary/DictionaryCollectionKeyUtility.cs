using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class DictionaryCollectionKeyUtility
    {
        /// <returns>是否添加成功</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddValueToValueCollection<TKey, TValue, TCollection>(
            this IDictionary<TKey, TCollection> dict, TKey key, TValue value)
            where TCollection : ICollection<TValue>, new()
        {
            if (dict.TryGetValue(key, out var collection) == false)
            {
                collection = new() { value };
                dict.Add(key, collection);
                return true;
            }

            var oldCount = collection.Count;
            collection.Add(value);
            return oldCount != collection.Count;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddValueToValueCollection<TKey, TValue, TCollection>(
            this IDictionary<TKey, TCollection> dict, TKey key, TValue value, out bool firstAdded)
            where TCollection : ICollection<TValue>, new()
        {
            if (dict.TryGetValue(key, out var collection) == false)
            {
                collection = new() { value };
                dict.Add(key, collection);
                firstAdded = true;
                return true;
            }

            var oldCount = collection.Count;
            firstAdded = oldCount == 0;
            collection.Add(value);
            return oldCount != collection.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RemoveValueAndRemoveKeyIfEmpty<TKey, TValue, TCollection>(
            this IDictionary<TKey, TCollection> dict, TKey key, TValue value)
            where TCollection : ICollection<TValue>
        {
            if (dict.TryGetValue(key, out var collection) == false)
            {
                return false;
            }

            var removed = collection.Remove(value);

            if (collection.Count == 0)
            {
                dict.Remove(key);
            }

            return removed;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RemoveValueAndRemoveKeyIfEmpty<TKey, TValue, TCollection>(
            this IDictionary<TKey, TCollection> dict, TKey key, TValue value, out bool collectionRemoved)
            where TCollection : ICollection<TValue>
        {
            if (dict.TryGetValue(key, out var collection) == false)
            {
                collectionRemoved = false;
                return false;
            }
            
            var removed = collection.Remove(value);

            if (collection.Count == 0)
            {
                dict.Remove(key);
                collectionRemoved = true;
            }
            else
            {
                collectionRemoved = false;
            }

            return removed;
        }
    }
}