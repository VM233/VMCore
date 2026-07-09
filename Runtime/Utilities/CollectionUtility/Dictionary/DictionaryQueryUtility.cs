using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class DictionaryQueryUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetValuesAs<TKey, TValue, TKeysCollection, TTargetValue>(
            this IReadOnlyDictionary<TKey, TValue> dictionary, TKeysCollection keys,
            ICollection<TTargetValue> targetValues)
            where TKeysCollection : IEnumerable<TKey>
        {
            foreach (var key in keys)
            {
                if (dictionary.TryGetValue(key, out var value) && value is TTargetValue targetValue)
                {
                    targetValues.Add(targetValue);
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetValuesAs<TKey, TValue, TTargetValue>(this IReadOnlyDictionary<TKey, TValue> dictionary,
            ICollection<TTargetValue> targetValues)
        {
            foreach (var value in dictionary.Values)
            {
                if (value is TTargetValue targetValue)
                {
                    targetValues.Add(targetValue);
                }
            }
        }
    }
}