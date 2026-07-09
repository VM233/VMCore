using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class DisposableCollectionUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DisposeAndClear<TDisposable>(this ICollection<TDisposable> collection)
            where TDisposable : IDisposable
        {
            foreach (var disposable in collection)
            {
                disposable.Dispose();
            }

            collection.Clear();
        }
    }
}