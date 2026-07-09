using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class CopyToUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyTo<TSource, T>(this TSource source, Span<T> destination)
            where TSource : IReadOnlyList<T>
        {
            for (int i = 0; i < source.Count; i++)
            {
                destination[i] = source[i];
            }
        }
    }
}