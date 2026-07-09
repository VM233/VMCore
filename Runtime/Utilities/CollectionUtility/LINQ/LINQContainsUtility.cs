using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VMFramework.Core.Linq
{
    public static class LINQContainsUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsSame<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ContainsSame(item => item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsSame<T, TSelector>(this IEnumerable<T> enumerable,
            Func<T, TSelector> selector)
        {
            var array = enumerable.ToArray();

            if (array.Length <= 1)
            {
                return false;
            }

            return array.Distinct(selector).Count() < array.Length;
        }
    }
}