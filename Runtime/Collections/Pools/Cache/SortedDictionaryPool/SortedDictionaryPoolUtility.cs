using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core.Pools
{
    public static class SortedDictionaryPoolUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReturnToSharedPool<TKey, TValue>(this SortedDictionary<TKey, TValue> dictionary) =>
            SortedDictionaryPool<TKey, TValue>.Shared.Return(dictionary);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReturnToDefaultPool<TKey, TValue>(this SortedDictionary<TKey, TValue> dictionary) =>
            SortedDictionaryPool<TKey, TValue>.Default.Return(dictionary);
    }
}