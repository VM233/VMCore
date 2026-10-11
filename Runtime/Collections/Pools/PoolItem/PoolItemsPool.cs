using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace VMFramework.Core.Pools
{
    /// <summary>
    /// This pool is not thread-safe.
    /// </summary>
    public abstract class PoolItemsPool<TPoolItem> : NormalPool<TPoolItem> where TPoolItem : IPoolItem
    {
        protected readonly Queue<TPoolItem> pool = new();

        public override int Count => pool.Count;

        protected abstract TPoolItem CreateItem();

        public override int Capacity { get; }

        protected PoolItemsPool(int capacity)
        {
            Capacity = capacity;
        }

        public sealed override TPoolItem Get(out bool isFreshlyCreated)
        {
            if (pool.TryDequeue(out TPoolItem item))
            {
                isFreshlyCreated = false;
                try
                {
                    item.OnGet();
                    return item;
                }
                catch (Exception primary)
                {
                    try { Return(item); }
                    catch (Exception retirement)
                    {
                        throw new PoolItemAcquisitionException(this, item, "get.return", primary, retirement);
                    }
                    throw;
                }
            }

            item = CreateItem();
            isFreshlyCreated = true;

            return item;
        }

        public sealed override bool Return(TPoolItem item) => Return(item, null);

        public bool Return(TPoolItem item, Action<TPoolItem> retirementPublisher)
        {
            Exception firstFailure = null;
            List<Exception> failures = null;
            try { item.OnReturn(); }
            catch (Exception failure) { firstFailure = failure; }

            try { retirementPublisher?.Invoke(item); }
            catch (Exception failure)
            {
                failures = new List<Exception>();
                if (firstFailure != null) failures.Add(firstFailure);
                failures.Add(failure);
            }

            if (firstFailure == null && failures == null && pool.Count < Capacity)
            {
                pool.Enqueue(item);
                return true;
            }

            try { item.OnClear(); }
            catch (Exception failure)
            {
                if (firstFailure == null && failures == null) firstFailure = failure;
                else
                {
                    if (failures == null)
                    {
                        failures = new List<Exception> { firstFailure };
                    }
                    failures.Add(failure);
                }
            }

            if (failures != null)
                throw new PoolItemAcquisitionException(this, item, "return.retirement", failures);
            if (firstFailure != null) ExceptionDispatchInfo.Capture(firstFailure).Throw();
            return false;
        }

        public sealed override void Clear()
        {
            var held = pool.ToArray();
            pool.Clear();
            List<Exception> failures = null;
            for (int index = 0; index < held.Length; index++)
            {
                var item = held[index];
                held[index] = default;
                try { item.OnClear(); }
                catch (Exception failure)
                {
                    (failures ??= new()).Add(new PoolItemAcquisitionException(this, item,
                        "clear.storage", failure));
                }
            }
            if (failures?.Count == 1) ExceptionDispatchInfo.Capture(failures[0]).Throw();
            if (failures?.Count > 1)
                throw new AggregateException("Clearing the entry-time pool storage failed.", failures);
        }

        public sealed override IEnumerator<TPoolItem> GetEnumerator() => pool.GetEnumerator();
    }
}
