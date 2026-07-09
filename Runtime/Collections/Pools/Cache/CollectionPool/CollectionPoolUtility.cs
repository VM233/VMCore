using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core.Pools
{
    public static class CollectionPoolUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAndReturnToDefaultPoolIfCountZero<TCollection, TItem>(this TCollection collection,
            TItem item)
            where TCollection : class, ICollection<TItem>, new()
        {
            if (collection.Remove(item) == false)
            {
                return;
            }
            
            if (collection.Count == 0)
            {
                CollectionPool<TCollection>.Default.Return(collection);
            }
        }
    }
}