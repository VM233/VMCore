using System;
using System.Collections.Generic;

namespace VMFramework.Core.Pools
{
    public class CollectionPoolFactory<TCollection, TValue>
        where TCollection : class, ICollection<TValue>, new()
    {
        public static Func<TCollection> CreateFromDefaultPool { get; } = () =>
        {
            var collection = CollectionPool<TCollection>.Default.Get();
            collection.Clear();
            return collection;
        };

        public static Func<TCollection> CreateFromSharedPool { get; } = () =>
        {
            var collection = CollectionPool<TCollection>.Shared.Get();
            collection.Clear();
            return collection;
        };
    }
}