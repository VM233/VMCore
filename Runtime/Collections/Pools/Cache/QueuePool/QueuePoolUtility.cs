using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core.Pools
{
    public static class QueuePoolUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReturnToSharedPool<T>(this Queue<T> list) => QueuePool<T>.Shared.Return(list);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReturnToDefaultPool<T>(this Queue<T> list) => QueuePool<T>.Default.Return(list);
    }
}