using System;
using System.Collections.Generic;

namespace VMFramework.Core.Pools
{
    public class CollectionPoolFactory<TCollection, TValue>
        where TCollection : class, ICollection<TValue>, new()
    {
        public static Func<TCollection> CreateFromDefaultPool => DefaultStorage.Create;

        public static Func<TCollection> CreateFromSharedPool => SharedStorage.Create;

        private static class DefaultStorage
        {
            internal static readonly Func<TCollection> Create = () =>
            {
                var collection = CollectionPool<TCollection>.Default.Get();
                collection.Clear();
                return collection;
            };

            // Publish only the factory delegate that was requested.
            static DefaultStorage()
            {
            }
        }

        private static class SharedStorage
        {
            internal static readonly Func<TCollection> Create = () =>
            {
                var collection = CollectionPool<TCollection>.Shared.Get();
                collection.Clear();
                return collection;
            };

            static SharedStorage()
            {
            }
        }
    }
}
