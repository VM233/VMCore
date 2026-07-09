using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class CollectionContainsUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsAny<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                if (collection.Contains(item))
                {
                    return true;
                }
            }
            
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsAll<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {     
                if (collection.Contains(item) == false)
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}