using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class DictionaryLookupUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryAdd<TKey, TItem, TCollection, TLookupDictionary>(
            this IDictionary<TKey, TCollection> dictionary, TLookupDictionary lookup, TKey key, TItem item)
            where TCollection : ISet<TItem>, new()
            where TLookupDictionary : IDictionary<TItem, TKey>
        {
            if (dictionary.TryGetValue(key, out var collection) == false)
            {
                collection = new() { item };
                dictionary.Add(key, collection);
                lookup.Add(item, key);
                return true;
            }

            if (collection.Add(item))
            {
                lookup.Add(item, key);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRemove<TKey, TItem, TCollection>(this IDictionary<TKey, TCollection> dictionary,
            IDictionary<TItem, TKey> lookup, TItem item)
            where TCollection : ICollection<TItem>
        {
            if (lookup.TryGetValue(item, out var key) == false)
            {
                return false;
            }
            
            dictionary[key].Remove(item);
            lookup.Remove(item);
            
            return true;
        }
    }
}