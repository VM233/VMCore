namespace VMFramework.Core.Pools
{
    public class CollectionPool<TCollection>
        where TCollection : class, new()
    {
        /// <summary>
        /// A pool that can be used by a single thread. The pool is not thread-safe.
        /// </summary>
        public static DefaultPool<TCollection> Default => DefaultStorage.Pool;

        /// <summary>
        /// A pool that can be shared across multiple threads. The pool is thread-safe.
        /// </summary>
        public static DefaultConcurrentPool<TCollection> Shared => SharedStorage.Pool;

        private static class DefaultStorage
        {
            internal static readonly DefaultPool<TCollection> Pool = new(new DefaultPoolPolicy<TCollection>(), 500);

            // An explicit initializer prevents CLR beforefieldinit from initializing an unrequested pool.
            static DefaultStorage()
            {
            }
        }

        private static class SharedStorage
        {
            internal static readonly DefaultConcurrentPool<TCollection> Pool = new(new DefaultPoolPolicy<TCollection>());

            // The shared pool has its own first-access lifetime, independent of Default.
            static SharedStorage()
            {
            }
        }
    }
}
