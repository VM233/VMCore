using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class DictionaryValueCreateUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
            where TValue : new()
        {
            if (dict.TryGetValue(key, out var value))
            {
                return value;
            }

            value = new TValue();
            dict.Add(key, value);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetOrCreateFromFactory<TKey, TValue, TDictionary>(this TDictionary dict, TKey key,
            Func<TValue> factory)
            where TDictionary : IDictionary<TKey, TValue>
        {
            if (dict.TryGetValue(key, out var value))
            {
                return value;
            }

            value = factory();
            dict.Add(key, value);
            return value;
        }
    }
}