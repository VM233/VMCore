using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class CollectionCompareUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SatisfyCompare<T>(this IReadOnlyList<T> list, [DisallowNull] Comparison<T> compare)
        {
            for (int i = 1; i < list.Count; i++)
            {
                if (compare(list[i - 1], list[i]) > 0)
                {
                    return false;
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SatisfyCompare<T>(this IEnumerable<T> enumerable, [DisallowNull] Comparison<T> compare)
        {
            bool hasLast = false;
            T last = default;
            foreach (T current in enumerable)
            {
                if (hasLast == false)
                {
                    last = current;
                    hasLast = true;
                    continue;
                }
                
                if (compare(last, current) > 0)
                {
                    return false;
                }
                
                last = current;
            }
            
            return true;
        }
    }
}