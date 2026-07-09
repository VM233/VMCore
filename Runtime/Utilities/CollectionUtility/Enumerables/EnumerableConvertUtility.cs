using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class EnumerableConvertUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerator<T> AsEnumerator<T>(this T value)
        {
            yield return value;
        }
    }
}