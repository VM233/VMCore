using System;

namespace VMFramework.Core.Pools
{
    /// <summary>
    /// Represents a policy for managing pooled items.
    /// </summary>
    public interface IPoolPolicy<TItem>
    {
        /// <summary>
        /// Runs some processing when an item is about to be retrieved from the pool.
        /// </summary>
        public Func<TItem, TItem> PreGet { get; }

        /// <summary>
        /// Create a <typeparamref name="TItem"/>.
        /// </summary>
        public Func<TItem> Create { get; }

        /// <summary>
        /// Runs some processing when an item was returned to the pool.
        /// Can be used to reset the state of an item and indicate if the item should be returned to the pool.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> if the item should be returned to the pool.
        /// <see langword="false" /> if it's not possible/desirable for the pool to keep the item.
        /// </returns>
        public Func<TItem, bool> Return { get; }

        public Action<TItem> Clear { get; }
    }
}