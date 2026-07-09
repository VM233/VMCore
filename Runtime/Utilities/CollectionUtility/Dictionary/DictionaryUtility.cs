using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class DictionaryUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValueAs<TKey, TValue, TTarget>(this IReadOnlyDictionary<TKey, TValue> dict, TKey key,
            out TTarget value)
        {
            if (dict.TryGetValue(key, out var valueAsObject) == false)
            {
                value = default;
                return false;
            }

            if (valueAsObject is TTarget target)
            {
                value = target;
                return true;
            }

            value = default;
            return false;
        }

        #region Get Value Or Default

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TValue> GetValuesOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict,
            IEnumerable<TKey> keys, TValue defaultValue = default)
        {
            return keys.Select(key => dict.TryGetValue(key, out var value) ? value : defaultValue);
        }

        #endregion

        #region Build Dictionary

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDictionary<TValue, IList<TKey>> BuildValuesDictionary<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary)
        {
            var result = new Dictionary<TValue, IList<TKey>>();

            foreach (var kvp in dictionary)
            {
                if (result.ContainsKey(kvp.Value) == false)
                {
                    result[kvp.Value] = new List<TKey>();
                }

                result[kvp.Value].Add(kvp.Key);
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDictionary<TKey, IList<TValue>> BuildDictionary<TItem, TKey, TValue>(
            this IEnumerable<TItem> enumerable, Func<TItem, (TKey, TValue)> keyValueSelector)
        {
            var result = new Dictionary<TKey, IList<TValue>>();

            foreach (var item in enumerable)
            {
                var (key, value) = keyValueSelector(item);

                if (result.TryGetValue(key, out var list) == false)
                {
                    list = new List<TValue>();
                    result.Add(key, list);
                }

                list.Add(value);
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDictionary<TKey, IList<TValue>> BuildSortedDictionary<TItem, TKey, TValue>(
            this IEnumerable<TItem> enumerable, Func<TItem, (TKey, TValue)> keyValueSelector,
            IComparer<TKey> comparer = null)
        {
            IDictionary<TKey, IList<TValue>> result;

            if (comparer == null)
            {
                result = new SortedDictionary<TKey, IList<TValue>>();
            }
            else
            {
                result = new SortedDictionary<TKey, IList<TValue>>(comparer);
            }

            foreach (var item in enumerable)
            {
                var (key, value) = keyValueSelector(item);

                if (result.TryGetValue(key, out var list) == false)
                {
                    list = new List<TValue>();
                    result.Add(key, list);
                }

                list.Add(value);
            }

            return result;
        }

        #endregion

        #region Examine

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ExamineKey<TKey, TValue>(this IDictionary<TKey, TValue> dict, Func<TKey, TKey> func)
            where TKey : struct
        {
            foreach (var key in dict.Keys.ToArray())
            {
                var newKey = func(key);

                var oldValue = dict[key];
                dict.Remove(key);
                dict[newKey] = oldValue;
            }
        }

        #endregion
    }
}