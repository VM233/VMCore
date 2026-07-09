using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class EnumerableQueryUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetTarget<TEnumerable, TTarget>(this TEnumerable enumerable, out TTarget value)
            where TEnumerable : IEnumerable
        {
            foreach (var item in enumerable)
            {
                if (item is TTarget target)
                {
                    value = target;
                    return true;
                }
            }
            
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetTarget<T, TTarget>(this ReadOnlySpan<T> span, out TTarget value)
        {
            foreach (var item in span)
            {
                if (item is TTarget target)
                {
                    value = target;
                    return true;
                }
            }
            
            value = default;
            return false;
        }
    }
}