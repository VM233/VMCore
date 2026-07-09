using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core.Linq
{
    public static class LINQFirstUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FirstOrDefault<T, TTarget>(this IEnumerable<T> enumerable, out TTarget target)
        {
            foreach (var item in enumerable)
            {
                if (item is TTarget targetItem)
                {
                    target = targetItem;
                    return true;
                }
            }
            
            target = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryFirstNotNullOrEmpty<TEnumerable>(this TEnumerable enumerable, out string first)
            where TEnumerable : IEnumerable<string>
        {
            foreach (var str in enumerable)
            {
                if (str.IsNullOrEmpty() == false)
                {
                    first = str;
                    return true;
                }
            }
            
            first = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryFirstNotUnityNull<T>(this IEnumerable<T> enumerable, out T first)
        {
            foreach (var item in enumerable)
            {
                if (item.IsUnityNull() == false)
                {
                    first = item;
                    return true;
                }
            }
            
            first = default;
            return false;
        }
    }
}